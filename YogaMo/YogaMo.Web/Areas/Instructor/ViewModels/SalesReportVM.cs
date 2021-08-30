using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.Areas.Instructor.ViewModels
{
    public class SalesReportVM
    {
        public List<string> WeekDaysList { get; set; } = new List<string>();
        public List<decimal> DailyRevenue { get; set; } = new List<decimal>();

        public List<string> MonthsList { get; set; } = new List<string>();

        public List<decimal> MonthRevenueMaleUsers { get; set; } = new List<decimal>();
        public List<decimal> MonthRevenueFemaleUsers { get; set; } = new List<decimal>();

        public int? Year { get; set; }
        public DateTime? Date { get; set; }
        public List<SelectListItem> YearsList { get; internal set; }
        public int TotalProductsSold { get; internal set; }
        public int TotalOrders { get; internal set; }
        public decimal TotalRevenue { get; internal set; }
        public int TotalClients { get; internal set; }
        public List<string> CategoryList { get; internal set; }
        public List<decimal> RevenuePerCategory { get; internal set; }
        public List<decimal> RevenuePerProduct { get; internal set; }
        public List<string> ProductNamesList { get; internal set; }
    }
}
