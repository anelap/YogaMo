using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public int? OrderStatusId { get; set; }

        public Clients Client { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
