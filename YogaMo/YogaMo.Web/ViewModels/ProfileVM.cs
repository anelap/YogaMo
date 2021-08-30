using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.ViewModels
{
    public class ProfileVM
    {
        [Display(Name= "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name= "Last Name")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Username { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public City City { get; set; }
        [Display(Name="City")]
        public int? CityId { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Name => FirstName + " " + LastName;
    }
}
