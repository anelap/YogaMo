using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public City City { get; set; }
        public int? CityId { get; set; }
        public byte[] ProfilePicture { get; set; }

        public string FullName => FirstName + " " + LastName;

        public override string ToString()
        {
            return FullName;
        }
    }
}
