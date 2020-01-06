using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
            Rating = new HashSet<Rating>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int QuantityStock { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public byte[] SlikaThumb { get; set; }
        public bool? Status { get; set; }

        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
        public ICollection<Rating> Rating { get; set; }
    }
}
