using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace employee_training_tool.Models
{
    public class LearningPath
    {
        public int LearningPathId { get; set; }
        public int AuthorId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        public ICollection<LearningPathTask> Tasks { get; set; }
        public ApplicationUser Author { get; set; }
    }
}