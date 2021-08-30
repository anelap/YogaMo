using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YogaMo.WebAPI.Database
{
    public partial class Yoga
    {
        public Yoga()
        {
            YogaClass = new HashSet<YogaClass>();
        }

        public int YogaId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual ICollection<YogaClass> YogaClass { get; set; }
    }
}
