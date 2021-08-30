using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.ViewModels
{
    public class YogaClassCreateVM
{
        public int YogaClassId { get; set; }
        [Display(Name = "Yoga")]
        public int? YogaId { get; set; }
        public string Day { get; set; }
        [Display(Name = "Instructor")]
        public int? InstructorId { get; set; }
        [Display(Name = "Yoga Hall")]
        public int? YogaHallId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "From")]
        public TimeSpan TimeFrom { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "To")]
        public TimeSpan TimeTo { get; set; }

        public virtual Yoga Yoga { get; set; }
        public virtual YogaHall YogaHall { get; set; }
    }
}
