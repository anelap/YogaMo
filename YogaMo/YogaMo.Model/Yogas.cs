using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Yogas
    {
        public int YogaId { get; set; }
        public string Name { get; set; }
        public int? InstructorId { get; set; }
        public string Description { get; set; }

        public Instructors Instructor { get; set; }
    }
}
