using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
