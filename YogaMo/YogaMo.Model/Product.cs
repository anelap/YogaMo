using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int QuantityStock { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public bool Status { get; set; }
        public string PriceStr => "$" + Price.ToString("0.00");

        public double AverageRating { get; set; }
    }
}
