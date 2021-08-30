using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Web.Areas.Administrator.ViewModels
{
    public class CreateInstructorVM: InstructorVM
    {
       
        [DataType(DataType.Password)]
        [Required]
        public new string Password { get; set; }
      
    }
}
