using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.ViewModels
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int QuantityStock { get; set; }
        public int ProductsSold { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 1;
        public string Size { get; set; }
        public string Color { get; set; }
        public Category Category { get; set; }
        public double AverageRating { get; set; }

        public List<string> Sizes { get; set; }
        public List<Model.Product> RecommendedProducts { get; set; }
    }
}
