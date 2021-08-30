using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [MaxLength(50)]
        public string CustomGender { get; set; }

    }
}
