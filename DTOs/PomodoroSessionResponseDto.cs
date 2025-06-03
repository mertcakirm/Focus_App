namespace Focus_App.DTOs
{
    public class PomodoroSessionResponseDto
    {
        public int Id { get; set; }
        public bool BreakUsed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}