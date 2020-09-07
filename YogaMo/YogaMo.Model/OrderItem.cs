using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public bool HasSize => !string.IsNullOrEmpty(Size);


}
}
