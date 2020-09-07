using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class YogaClassInsertRequest
    {
        public int YogaId { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}
