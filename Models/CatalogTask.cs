namespace employee_training_tool.Models
{
    public class CatalogTask
    {
        public int CatalogTaskId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
        public TaskTypes TaskType { get; set; }
        public ApplicationUser Author { get; set; }
    }
}