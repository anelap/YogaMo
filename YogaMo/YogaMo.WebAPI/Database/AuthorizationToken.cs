using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Database
{
    public class AuthorizationToken
    {
        public Guid Id { get; set; }

        public Client Client { get; set; }
        public int? ClientId { get; set; }
        public Instructor Instructor { get; set; }
        public int? InstructorId { get; set; }
        public Administrator Administrator { get; set; }
        public int? AdministratorId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
