using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Clients
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public bool? Status { get; set; }
        public int? CityId { get; set; }

        public Cities City { get; set; }
    }
}
