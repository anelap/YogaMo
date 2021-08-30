using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorization(isClient: true)]
    public class OrdersController : Controller
    {
        private readonly _150222Context _context;

        public OrdersController(_150222Context context)
        {
            _context = context;
        }

        // GET: Client/Orders
        public async Task<IActionResult> Index(OrderStatus? OrderStatus)
        {
            var Client = HttpContext.GetLoggedInClient();

            var query = _context.Order.Include(o => o.Client)
                .Where(x=>x.ClientId == Client.ClientId)
                .OrderByDescending(x=>x.Date)
                .AsQueryable();
            if (OrderStatus != null)
                query = query.Where(x => x.OrderStatus == OrderStatus.Value);

            ViewBag.OrderStatus = OrderStatus;

            return View(await query.ToListAsync());
        }

        // GET: Client/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Client = HttpContext.GetLoggedInClient();

            var order = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.OrderItem)
                .Include("OrderItem.Product")
                .Where(x=>x.ClientId == Client.ClientId)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> CancelOrder(int Id)
        {
            var Order = await _context.Order.FirstOrDefaultAsync(m => m.OrderId == Id);
            if(Order != null && Order.OrderStatus == OrderStatus.Processing)
            {
                Order.OrderStatus = OrderStatus.Canceled;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
