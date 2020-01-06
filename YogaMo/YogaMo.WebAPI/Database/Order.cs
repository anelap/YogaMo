using System;
using System.Collections.Generic;

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
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public int? OrderStatusId { get; set; }

        public Client Client { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
