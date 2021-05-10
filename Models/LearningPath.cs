using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class LearningPath
    {
        public int LearningPathId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<LearningPathTask> Tasks { get; set; }
        public ApplicationUser Author { get; set; }
    }
}