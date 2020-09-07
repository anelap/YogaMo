using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class YogaClasses
    {
        public int YogaClassId { get; set; }
        public int? YogaId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        public Yogas Yoga { get; set; }
    }
}
