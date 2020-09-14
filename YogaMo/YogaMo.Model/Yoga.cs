using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model
{
    public class Yoga
    {
        public int YogaId { get; set; }
        public string Name { get; set; }
        public Instructor Instructor { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
