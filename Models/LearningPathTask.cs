namespace employee_training_tool.Models
{
    public class LearningPathTask
    {
        public int LearningPathTaskId { get; set; }
        public int LearningPathId { get; set; }
        public int CatalogTaskId { get; set; }
        public CatalogTask CatalogTask { get; set; }

        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskTypes TaskType { get; set; }
    }
}