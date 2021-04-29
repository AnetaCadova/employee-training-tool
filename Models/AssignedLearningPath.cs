using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class AssignedLearningPath
    {
        public int AssignedLearningPathId { get; set; }
        public int OriginalLearningPathId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual LearningPath OriginalLearningPath { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<AssignedTask> Tasks { get; set; }
    }
}