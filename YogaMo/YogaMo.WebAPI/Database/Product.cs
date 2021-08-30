using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name= "Category")]
        public int? CategoryId { get; set; }
        [Display(Name="Quantity in stock")]
        public int QuantityStock { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public bool Status { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
