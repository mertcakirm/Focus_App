namespace Focus_App.DTOs
{
    public class TaskItemDto
    {
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}