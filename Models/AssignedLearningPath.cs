using System.Collections.Generic;

namespace employee_training_tool.Models
{
    public class AssignedLearningPath
    {
        public int AssignedLearningPathId { get; set; }
        public int OriginalLearningPathId { get; set; }
        public int MentorId { get; set; }
        public int NewComerId { get; set; }
        public int EnrollmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ApplicationUser Mentor { get; set; }
        public ApplicationUser NewComer { get; set; }
        public Enrollment Enrollment { get; set; }
        public LearningPath OriginalLearningPath { get; set; }
        public ICollection<AssignedTask> Tasks { get; set; }
    }
}