namespace employee_training_tool.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public virtual Newcomer NewComer { get; set; }
        public virtual Mentor Mentor { get; set; }
        public virtual AssignedLearningPath LearningPath { get; set; }
    }
}