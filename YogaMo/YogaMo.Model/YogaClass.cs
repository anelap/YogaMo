using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class YogaClass
    {
        public int YogaClassId { get; set; }
        public int? YogaId { get; set; }
        public Yoga Yoga { get; set; }
        public string Day { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public string Time => TimeFrom.ToString(@"hh\:mm") + "-" + TimeTo.ToString(@"hh\:mm");

        public Instructor Instructor { get; set; }
        public int? InstructorId { get; set; }
    }
}
