using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Areas.Instructor.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class ReportController : Controller
    {
        private readonly _150222Context _context;
        private readonly IMapper mapper;

        public ReportController(_150222Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public IActionResult Index(SalesReportVM VM)
        {
            LoadWeeklySales(VM);
            LoadMonthlySales(VM);
            LoadTotals(VM);
            LoadCategoryRevenue(VM);
            LoadProductRevenue(VM);
            return View(VM);
        }

        private void LoadProductRevenue(SalesReportVM VM)
        {
            VM.RevenuePerProduct = _context.OrderItem
                .Where(x => x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)
                .GroupBy(x=>x.Product.Name)
                .Select(x => x.Sum(s=>s.Price * s.Quantity))
                .OrderByDescending(x=>x)
                .Take(5)
                .ToList();

            VM.ProductNamesList = _context.OrderItem
                .Where(x => x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)
                .GroupBy(x=>x.Product.Name)
                .OrderByDescending(x => x.Sum(s => s.Price * s.Quantity))
                .Select(x => x.Key)
                .Take(5)
                .ToList();

        }

        private void LoadCategoryRevenue(SalesReportVM VM)
        {
            VM.CategoryList = _context.Category.OrderBy(x => x.CategoryId).Select(x => x.Name).ToList();
            VM.RevenuePerCategory = _context.Category.OrderBy(x => x.CategoryId).Select(
                x => _context.OrderItem
                .Where(x => x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)
                .Where(y => y.Product.CategoryId == x.CategoryId).Sum(y => y.Price * y.Quantity)
            ).ToList();
        }

        private void LoadTotals(SalesReportVM VM)
        {
            VM.TotalProductsSold = _context.OrderItem
                .Where(x => x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)
                .Count();

            VM.TotalOrders = _context.Order
                .Where(x=>x.OrderStatus != OrderStatus.Canceled)
                .Count();

            VM.TotalRevenue = _context.OrderItem
                .Where(x => x.Order.OrderStatus == OrderStatus.Confirmed || x.Order.OrderStatus == OrderStatus.Completed)
                .Sum(x => x.Price * x.Quantity);

            VM.TotalClients = _context.Client.Count();
        }

        private void LoadMonthlySales(SalesReportVM VM)
        {
            if (VM.Year is null)
                VM.Year = DateTime.Today.Year;

            var Years = _context.Order.Select(x => x.Date.Year).Distinct().ToList();
            Years.Sort();

            VM.YearsList = Years.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString()
            }).ToList();

            for (int i = 1; i <= 12; i++)
            {
                var revenue_male = _context.Order
                    .Where(x=>x.Client.Gender.ToLower() == "male")
                    .Where(x => x.Date.Month == i && x.Date.Year == VM.Year.Value)
                    .Sum(x => x.TotalPrice);

                var revenue_female = _context.Order
                    .Where(x=>x.Client.Gender.ToLower() == "female")
                    .Where(x => x.Date.Month == i && x.Date.Year == VM.Year.Value)
                    .Sum(x => x.TotalPrice);

                VM.MonthRevenueMaleUsers.Add(revenue_male);
                VM.MonthRevenueFemaleUsers.Add(revenue_female);

                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                VM.MonthsList.Add(monthName);
            }
        }

        private void LoadWeeklySales(SalesReportVM VM)
        {
            if (VM.Date is null)
                VM.Date = DateTime.Today;

            DateTime date = VM.Date.Value;
            for (int i = 0; i < 7; i++)
            {
                var revenue_daily = _context.Order
                    .Where(x => x.Date.Date == date)
                    .Sum(x => x.TotalPrice);

                VM.DailyRevenue.Add(revenue_daily);

                string DayOfWeekShort = date.DayOfWeek.ToString().Substring(0, 3);

                VM.WeekDaysList.Add(DayOfWeekShort);
                date = date.AddDays(-1);
            }

            VM.WeekDaysList.Reverse();
            VM.DailyRevenue.Reverse();

        }
    }
}
