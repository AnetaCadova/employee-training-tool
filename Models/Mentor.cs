using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class Mentor : User
    {
        public virtual ICollection<Newcomer> Newcomers { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public Mentor()
        {
            UserRole = Role.Mentor;
        }
    }
}