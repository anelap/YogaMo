using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class OrderItemInsertRequest
    {
        public int ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
}
