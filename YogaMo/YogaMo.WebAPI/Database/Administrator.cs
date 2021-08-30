using System;
using System.Collections.Generic;

namespace YogaMo.WebAPI.Database
{
    public partial class Administrator
    {
        public int AdministratorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
