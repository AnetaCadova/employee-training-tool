namespace employee_training_tool.Models
{
    public class CatalogTask
    {
        public int CatalogTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskTypes TaskType { get; set; }
    }
}