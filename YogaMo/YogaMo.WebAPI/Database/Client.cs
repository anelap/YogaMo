using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaMo.WebAPI.Database
{
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
            Rating = new HashSet<Rating>();
        }

        public int ClientId { get; set; }
        [Display(Name="First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public string SanitizedGender => Gender.Substring(0, 1).ToUpper() + Gender.Substring(1).ToLower();
        public string FullName => FirstName + " " + LastName;
        public int? CityId { get; set; }
        [Display(Name = "Profile picture")]
        public byte[] ProfilePicture { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }

    }
}

