using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class LearningPath
    {
        public int LearningPathId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AssignedTask> Tasks { get; set; }
    }
}