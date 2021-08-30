using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.Areas.Administrator.ViewModels
{
    public class InstructorVM
    {
        public int InstructorId { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Active")]
        public bool Status { get; set; }
        public string Title { get; set; }
        [Display(Name = "Profile picture")]
        public byte[] ProfilePicture { get; set; }

        public bool RemoveProfilePicture { get; set; }
        public string Name => FirstName + " " + LastName;
    }
}
