﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class YogaInsertRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int InstructorId { get; set; }
        public string Description { get; set; }
    }
}
