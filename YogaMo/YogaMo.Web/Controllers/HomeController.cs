using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private const int DisplayPerPage = 6;

        public HomeController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public IActionResult Index()
        {
            var VM = LoadShopData(null);
            return View(VM);
        }

        [HttpGet("Category/{Page}/{Category?}")]
        public IActionResult Category(string Category, int Page)
        {
            var VM = LoadShopData(Category, Page);
            return PartialView("Category", VM);
        }

        private HomeVM LoadShopData(string SelectedCategory = null, int Page = 1)
        {
            int? CategoryId = null;
            if (SelectedCategory != null)
            {
                var CategoryObj = _context.Category.FirstOrDefault(x => x.Name == SelectedCategory);
                if (CategoryObj != null)
                    CategoryId = CategoryObj.CategoryId;
            }

            int TotalProducts = _context.Product
                .Where(x => CategoryId == null || x.CategoryId == CategoryId)
                .Where(x => x.Status == true)
                .Count();

            int TotalPages = TotalProducts / DisplayPerPage;
            if (TotalProducts % DisplayPerPage > 0)
                TotalPages++;

            return new HomeVM
            {
                Category = SelectedCategory,
                Categories = _context.Category.ToList(),
                Page = Page,
                TotalPages = TotalPages,
                Products = _context.Product
                .Where(x => CategoryId == null || x.CategoryId == CategoryId)
                .Where(x => x.Status == true)
                .Select(x => new ProductVM
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Price = x.Price,
                    Photo = x.Photo,
                    AverageRating = x.Rating.Average(y => (double?)y.Rating1) ?? 0.00,
                })
                .Skip((Page - 1) * DisplayPerPage)
                .Take(DisplayPerPage)
                .ToList()
            };
        }

        public IActionResult Dashboard()
        {
            var userInfo = HttpContext.GetLoggedInUser();
            if (userInfo != null)
            {
                string redirectUrl = null;

                if (userInfo.RoleName == "Administrator")
                {
                    redirectUrl = "/Administrator/Instructors";
                }
                else if (userInfo.RoleName == "Instructor")
                {
                    redirectUrl = "/Instructor/YogaClasses";
                }
                else if (userInfo.RoleName == "Client")
                {
                    redirectUrl = "/Client/YogaClasses";
                }

                if (redirectUrl != null)
                    return Redirect(redirectUrl);
            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Gallery()
        {
            return View(await _context.YogaPhoto.ToListAsync());
        }


        public async Task<IActionResult> Tutorials()
        {
            return View(await _context.YogaVideo.ToListAsync());
        }

        public IActionResult Logout()
        {
            HttpContext.Logout();
            return RedirectToAction(nameof(Index));
        }

       

    }
}
