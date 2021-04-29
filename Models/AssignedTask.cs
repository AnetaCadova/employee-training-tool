namespace employee_training_tool.Models
{
    public class AssignedTask
    {
        public int AssignedTaskId { get; set; }
        public int LearningPathId { get; set; }
        public int CatalogTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskTypes TaskType { get; set; }
        public virtual LearningPath LearningPath { get; set; }

    }
}