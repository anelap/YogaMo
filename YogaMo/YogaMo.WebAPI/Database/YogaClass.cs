using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaMo.WebAPI.Database
{
    public partial class YogaClass
    {
        public int YogaClassId { get; set; }
        public string Day { get; set; }
        [Display(Name="From")]
        public TimeSpan TimeFrom { get; set; }
        [Display(Name="To")]
        public TimeSpan TimeTo { get; set; }
        public string Time => TimeFrom.ToString(@"hh\:mm") + "-" + TimeTo.ToString(@"hh\:mm");
        [Display(Name = "Yoga")]
        public int? YogaId { get; set; }
        [Display(Name="Instructor")]
        public int? InstructorId { get; set; }

        [Display(Name = "Yoga Hall")]
        public int? YogaHallId { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual Yoga Yoga { get; set; }
        public virtual YogaHall YogaHall { get; set; }
    }
}
