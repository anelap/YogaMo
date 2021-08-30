using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaMo.WebAPI.Database
{
    public partial class Instructor
    {
        public Instructor()
        {
            YogaClass = new HashSet<YogaClass>();
        }

        public int InstructorId { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        [Display(Name = "Profile picture")]
        public byte[] ProfilePicture { get; set; }

        public string FullName => FirstName + " " + LastName;

        public virtual ICollection<YogaClass> YogaClass { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
