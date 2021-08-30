using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaMo.WebAPI.Database
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? ClientId { get; set; }
         
        [Display(Name="Price")]
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
