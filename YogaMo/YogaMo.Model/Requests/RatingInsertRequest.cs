using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class RatingInsertRequest
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Rating1 { get; set; }
    }
}
