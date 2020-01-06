using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public bool? Status { get; set; }
        public string Title { get; set; }
        public int? CityId { get; set; }

        public City City { get; set; }
    }
}
