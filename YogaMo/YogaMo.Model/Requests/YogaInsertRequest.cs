using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class YogaInsertRequest
    {
        public string Name { get; set; }
        public int InstructorId { get; set; }
        public string Description { get; set; }
    }
}
