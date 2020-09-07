using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class OrderInsertRequest
    {
        public int? ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
