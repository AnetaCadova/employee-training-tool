namespace employee_training_tool.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int NewComerId { get; set; }
        public int MentorId { get; set; }
        public int LearningPathId { get; set; }

        public virtual ApplicationUser NewComer { get; set; }
        public virtual ApplicationUser Mentor { get; set; }
        public virtual AssignedLearningPath LearningPath { get; set; }
    }
}