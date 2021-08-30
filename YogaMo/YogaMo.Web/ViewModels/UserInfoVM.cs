using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.ViewModels
{
    public class UserInfoVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
