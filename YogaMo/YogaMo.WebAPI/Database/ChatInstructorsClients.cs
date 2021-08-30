using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YogaMo.WebAPI.Database
{
    public class ChatInstructorsClients
    {
        public int Id { get; set; }
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public DateTime LastMessage { get; set; }
        [Required]
        [DefaultValue("0")]
        public string LastSeenInstructor { get; set; } = "0";
        [Required]
        [DefaultValue("0")]
        public string LastSeenClient { get; set; } = "0";
    }
}
