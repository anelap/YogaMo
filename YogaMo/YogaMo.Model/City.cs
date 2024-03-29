﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
