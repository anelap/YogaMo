using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.Model
{
    public class YogaHall
    {
        public int YogaHallId { get; set; }
        [Display(Name = "Yoga")]
        public int? YogaId { get; set; }

        public virtual Yoga Yoga { get; set; }
        public string Name { get; set; }
    }
}
