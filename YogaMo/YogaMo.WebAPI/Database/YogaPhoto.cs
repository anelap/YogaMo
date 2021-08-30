using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Database
{
    public class YogaPhoto
    {
        public int Id { get; set; }
        [Display(Name = "Photo")]
        public string PhotoFileName { get; set; }
    }
}
