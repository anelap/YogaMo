using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int? ProductId { get; set; }
        public int? ClientId { get; set; }
        public int Rating1 { get; set; }

        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
