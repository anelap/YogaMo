﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class InstructorInsertRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public bool? Status { get; set; }
        public string Title { get; set; }
     //   public int? CityId { get; set; }

       // public City City { get; set; }

        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
