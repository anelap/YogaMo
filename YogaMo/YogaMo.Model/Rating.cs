using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Rating1 { get; set; }
    }
}
