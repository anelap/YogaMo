using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class ProductSearchRequest
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
