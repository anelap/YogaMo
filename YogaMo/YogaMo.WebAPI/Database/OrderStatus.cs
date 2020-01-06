using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Order = new HashSet<Order>();
        }

        public int OrderStatusId { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
