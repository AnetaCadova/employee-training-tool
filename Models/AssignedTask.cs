namespace employee_training_tool.Models
{
    public class AssignedTask : Task
    {
        public virtual LearningPath LearningPath { get; set; }
        public TaskStatus Status { get; set; }
        public TaskTypes TaskTypes { get; set; }
    }
}