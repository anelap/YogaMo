using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class YogaClassInsertRequest
    {
        [Required]
        public int YogaId { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string Time { get; set; }
    }
}
