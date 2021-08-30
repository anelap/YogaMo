using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    [Area("Client")]
    [Authorization(isClient: true)]
    public class CartController : BaseController
    {
        public CartController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadCart()
        {
            var CurrentClient = HttpContext.GetLoggedInClient();
            var NewOrder = _context.Order.Where(x => x.ClientId == CurrentClient.ClientId && x.OrderStatus == OrderStatus.New).FirstOrDefault();

            if (NewOrder == null)
                return PartialView(new List<WebAPI.Database.OrderItem>());
            var Products = _context.OrderItem
                .Where(x => x.OrderId == NewOrder.OrderId)
                .Include(x => x.Product.Category)
                .ToList();

            return PartialView(Products);
        }

        public IActionResult RemoveItem(int Id)
        {
            var CurrentClient = HttpContext.GetLoggedInClient();

            var OrderItem = _context.OrderItem.Where(x => x.OrderItemId == Id && x.Order.ClientId == CurrentClient.ClientId).FirstOrDefault();
            if (OrderItem != null)
            {
                _context.Remove(OrderItem);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(LoadCart));
        }

        public IActionResult UpdateQuantity(int Id, int quantity)
        {
            var CurrentClient = HttpContext.GetLoggedInClient();

            var OrderItem = _context.OrderItem.Where(x => x.OrderItemId == Id && x.Order.ClientId == CurrentClient.ClientId).Include(x => x.Product).FirstOrDefault();
            if (OrderItem != null && quantity > 0 && quantity <= OrderItem.Product.QuantityStock)
            {
                OrderItem.Quantity = quantity;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(LoadCart));
        }

        public IActionResult Purchase(int Id)
        {
            var CurrentClient = HttpContext.GetLoggedInClient();

            var Order = _context.Order
                .Where(x => x.OrderId == Id && x.ClientId == CurrentClient.ClientId && (x.OrderStatus == OrderStatus.New || x.OrderStatus == OrderStatus.Processing))
                .Include(x => x.OrderItem)
                .FirstOrDefault();

            if (Order != null && Order.OrderItem.Count > 0)
            {
                if (Order.OrderStatus != OrderStatus.Processing)
                {
                    Order.OrderStatus = OrderStatus.Processing;
                    _context.SaveChanges();
                }
                return View(Order);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public bool ConfirmOrder([FromBody] Model.Requests.PaymentIntentCreateRequest request)
        {
            var Order = _context.Order
                .Where(x => x.OrderId == request.OrderId)
                .Include(x => x.OrderItem)
                .FirstOrDefault();

            if (Order == null) return false;

            var paymentIntents = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = Convert.ToInt32(Order.TotalPrice * 100),
                Currency = "usd",
                Confirm = true,
                PaymentMethod = request.PaymentMethodId,
                Metadata = new Dictionary<string, string>()
            };
            options.Metadata.Add("OrderId", request.OrderId.ToString());

            var paymentIntent = paymentIntents.Create(options);
            if (paymentIntent == null)
                return false;

            foreach (var charge in paymentIntent.Charges)
            {
                if (charge.Paid == true)
                {
                    Order.OrderStatus = WebAPI.Database.OrderStatus.Confirmed;
                    _context.SaveChanges();
                    TempData["Success"] = "You have successfully completed your order!";


                    foreach (var item in Order.OrderItem)
                    {
                        var product = _context.Product.Find(item.ProductId);
                        product.QuantityStock -= item.Quantity;
                    }
                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }


    }
}
