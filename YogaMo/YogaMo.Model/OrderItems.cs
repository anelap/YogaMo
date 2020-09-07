using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
