﻿using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public ICollection<City> City { get; set; }
    }
}
