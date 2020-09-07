using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class City
    {
        public City()
        {
            Client = new HashSet<Client>();
            Instructor = new HashSet<Instructor>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }

        public ICollection<Client> Client { get; set; }
        public ICollection<Instructor> Instructor { get; set; }
    }
}
