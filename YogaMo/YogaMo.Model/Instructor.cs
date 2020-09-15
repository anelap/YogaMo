using System;

namespace YogaMo.Model
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public bool? Status { get; set; }
        public string Title { get; set; }

        public byte[] ProfilePicture { get; set; }
        public string Name => FirstName + " " + LastName;

        public override string ToString()
        {
            return Name;
        }

    }
}
