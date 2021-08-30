using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.ViewModels
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public string Category { get; set; }
        public List<ProductVM> Products { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
