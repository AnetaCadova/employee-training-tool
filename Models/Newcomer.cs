using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class Newcomer : User
    {
        public Mentor Mentor;
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public Newcomer()
        {
            UserRole = Role.Newcomer;
        }
    }
}