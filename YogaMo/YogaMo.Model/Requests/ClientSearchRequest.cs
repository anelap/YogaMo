using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class ClientSearchRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string City { get; set; } //or CityId ??
    }
}
