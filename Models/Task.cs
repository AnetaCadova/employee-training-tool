namespace employee_training_tool.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public int LearningPathID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}