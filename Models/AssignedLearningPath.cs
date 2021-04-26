using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class AssignedLearningPath : LearningPath
    {
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}