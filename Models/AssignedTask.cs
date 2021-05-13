namespace employee_training_tool.Models
{
    public class AssignedTask
    {
        public int AssignedTaskId { get; set; }
        public int AssignedLearningPathId { get; set; }
        public int CatalogTaskId { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public TaskTypes TaskType { get; set; }
        public AssignedLearningPath AssignedLearningPath { get; set; }
    }
}