using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class ProductInsertRequest
    {
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int QuantityStock { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
        public bool? Status { get; set; }
    }
}
