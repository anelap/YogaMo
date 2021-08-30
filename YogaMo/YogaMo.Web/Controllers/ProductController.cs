using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly int MinimumRating = 3;
        private readonly int NumberOfRecommendations = 3;
        public ProductController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public IActionResult Index(int Id)
        {
            var Product = _context.Product
                .Where(x => x.ProductId == Id)
                .Include(x=>x.Category)
                .Include(x=>x.Rating)
                .FirstOrDefault();
            if (Product == null)
                return NotFound();

            ProductVM VM = _mapper.Map<ProductVM>(Product);
            VM.AverageRating = Product.Rating.Average(y => (double?)y.Rating1) ?? 0.00;
            VM.Sizes = new List<string> { "S", "M", "L", "XL" };
            VM.Size = "S";
            VM.RecommendedProducts = GetRecommendedProducts(Id);
            VM.ProductsSold = _context.OrderItem.Where(x => x.ProductId == Id && (x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)).Sum(x => x.Quantity);

            return View(VM);
        }


        [Authorization(isClient: true)]
        public IActionResult AddToCart(ProductVM request)
        {
            var CurrentClient = HttpContext.GetLoggedInClient();
            var entity = _mapper.Map<WebAPI.Database.OrderItem>(request);

            var order = _context.Order.Where(x=>x.ClientId == CurrentClient.ClientId && x.OrderStatus == OrderStatus.New).FirstOrDefault();

            var product = _context.Product.Find(request.ProductId);
            if (product == null)
            {
                return new BadRequestObjectResult(new { error = "Product not found" });
            }

            if (entity.Quantity < 0 || product.QuantityStock < entity.Quantity)
            {
                return new BadRequestObjectResult(new { error = "Product not available in requested quantity" });
            }

            if (order != null)
            {
                entity.OrderId = order.OrderId;
            }
            else
            {
                order = new WebAPI.Database.Order
                {
                    ClientId = CurrentClient.ClientId,
                    Date = DateTime.Now,
                    OrderStatus = WebAPI.Database.OrderStatus.New
                };
                _context.Order.Add(order);
                _context.SaveChanges();

                entity.OrderId = order.OrderId;
            }

            entity.Product = product;
            entity.Price = product.Price;

            _context.OrderItem.Add(entity);
            _context.SaveChanges();

            var TotalPrice = _context.OrderItem.Where(x => x.OrderId == order.OrderId).Sum(x => x.Price * x.Quantity);
            order.TotalPrice = TotalPrice;
            _context.SaveChanges();

            TempData["Success"] = "Added To Cart: <b>" + product.Name + "</b> (" + request.Quantity + "x)";

            return Ok();
        }

        [Authorization(isClient: true)]
        public IActionResult BuyNow(ProductVM request)
        {
            var CurrentClient = HttpContext.GetLoggedInClient();
            var entity = _mapper.Map<WebAPI.Database.OrderItem>(request);

            var order = _context.Order.Where(x=>x.ClientId == CurrentClient.ClientId && x.OrderStatus == OrderStatus.New).FirstOrDefault();

            var product = _context.Product.Find(request.ProductId);
            if (product == null)
            {
                return new BadRequestObjectResult(new { error = "Product not found" });
            }

            if (entity.Quantity < 0 || product.QuantityStock < entity.Quantity)
            {
                return new BadRequestObjectResult(new { error = "Product not available in requested quantity" });
            }

            if (order != null)
            {
                entity.OrderId = order.OrderId;
            }
            else
            {
                order = new WebAPI.Database.Order
                {
                    ClientId = CurrentClient.ClientId,
                    Date = DateTime.Now,
                    OrderStatus = WebAPI.Database.OrderStatus.New
                };
                _context.Order.Add(order);
                _context.SaveChanges();

                entity.OrderId = order.OrderId;
            }

            entity.Product = product;
            entity.Price = product.Price;

            _context.OrderItem.Add(entity);
            _context.SaveChanges();

            var TotalPrice = _context.OrderItem.Where(x => x.OrderId == order.OrderId).Sum(x => x.Price * x.Quantity);
            order.TotalPrice = TotalPrice;
            _context.SaveChanges();

            TempData["Success"] = "Added To Cart: <b>" + product.Name + "</b> (" + request.Quantity + "x)";

            return Ok();
        }

        public IActionResult RateProduct(int Id, int rating)
        {
            var Client = HttpContext.GetLoggedInClient();

            if(!_context.OrderItem.Any(x=>x.ProductId == Id && x.Order.ClientId == Client.ClientId && x.Order.OrderStatus == OrderStatus.Completed))
            {
                TempData["Error"] = "You cannot rate a product you have not bought.";
                return RedirectToAction(nameof(Index), new { Id });
            }

            var entity = _context.Rating.Where(x => x.ProductId == Id && x.ClientId == Client.ClientId).FirstOrDefault();
            if (entity != null)
            {
                entity.Rating1 = rating;
            }
            else
            {
                entity = new Rating
                {
                    ClientId = Client.ClientId,
                    ProductId = Id,
                    Rating1 = rating
                };

                _context.Rating.Add(entity);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), new { Id });
        }

        public List<Model.Product> GetRecommendedProducts(int ProductIdToExclude)
        {
            int ClientId = HttpContext.GetLoggedInClient()?.ClientId ?? 0;
            if (ClientId != 0)
            {
                List<Rating> ListOfRatings = _context.Rating.Where(x => x.ClientId == ClientId)
                    .Include(m => m.Product.Category)
                    .ToList();

                List<Rating> ListOfPositiveRatings = ListOfRatings
                    .Where(x => x.Rating1 >= MinimumRating)
                    .ToList();

                if (ListOfPositiveRatings.Count() > 0)
                {
                    List<Category> uniqueCategories = new List<Category>();
                    foreach (var Rating in ListOfPositiveRatings)
                    {
                        bool add = true;
                        for (int i = 0; i < uniqueCategories.Count; i++)
                        {
                            if (Rating.Product.CategoryId == uniqueCategories[i].CategoryId)
                            {
                                add = false;
                            }
                        }

                        if (add)
                        {
                            uniqueCategories.Add(Rating.Product.Category);
                        }
                    }

                    List<Product> ListOfRecommendedProducts = new List<Product>();
                    foreach (var Category in uniqueCategories)
                    {
                        var ProductsInCategory = _context.Product
                            .Where(x => x.QuantityStock > 0)
                            .Where(x => x.ProductId != ProductIdToExclude)
                            .Where(n => n.CategoryId == Category.CategoryId)
                            .Include(x => x.Category)
                            .ToList();

                        foreach (var Product in ProductsInCategory)
                        {
                            bool add = true;
                            for (int i = 0; i < ListOfRecommendedProducts.Count; i++)
                            {
                                if (Product.ProductId == ListOfRecommendedProducts[i].ProductId)
                                {
                                    add = false;
                                }
                            }

                            foreach (var Rating in ListOfRatings)
                            {
                                if (Product.ProductId == Rating.ProductId)
                                {
                                    add = false;
                                }
                            }

                            if (add)
                            {
                                ListOfRecommendedProducts.Add(Product);
                            }
                        }
                    }

                    if (ListOfRecommendedProducts.Count > 0)
                    {
                        var list1 = _mapper.Map<List<Model.Product>>(ListOfRecommendedProducts);

                        foreach (var entity in list1)
                        {
                            entity.AverageRating = _context.Rating.Where(x => x.ProductId == entity.ProductId).Average(x => (double?)x.Rating1) ?? 0;
                        }

                        list1 = list1.OrderByDescending(x => x.AverageRating).Take(NumberOfRecommendations).ToList().OrderBy(x => Guid.NewGuid()).ToList();

                        return list1;
                    }
                }
            }

            var ListOfAllProducts = _context.Product
                                        .Where(x => x.QuantityStock > 0)
                                        .Where(x => x.Status == true)
                                        .Where(x => x.ProductId != ProductIdToExclude)
                                        .Include(x => x.Category)
                                        .ToList();

            var list2 = _mapper.Map<List<Model.Product>>(ListOfAllProducts);

            foreach (var entity in list2)
            {
                entity.AverageRating = _context.Rating.Where(x => x.ProductId == entity.ProductId).Average(x => (double?)x.Rating1) ?? 0;
            }

            list2 = list2.OrderByDescending(x => x.AverageRating).Take(NumberOfRecommendations).ToList().OrderBy(x=>Guid.NewGuid()).ToList();

            return list2;
        }
    }
}
