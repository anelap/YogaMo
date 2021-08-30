using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Administrator.ViewModels
{
    public class ProductsIndexVM
    {
        public int CategoryId { get; set; }
        public List<ProductsVM> Rows { get; set; }

        public class ProductsVM
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            [Display(Name = "Category")]
            public int? CategoryId { get; set; }
            [Display(Name = "Quantity in stock")]
            public int QuantityStock { get; set; }
            public decimal Price { get; set; }
            public byte[] Photo { get; set; }
            public bool Status { get; set; }
            public Category Category { get; set; }

        }
    }
}
