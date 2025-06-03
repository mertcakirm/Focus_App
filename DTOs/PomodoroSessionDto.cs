namespace Focus_App.DTOs;

public class PomodoroSessionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int DurationMinutes { get; set; }
    public bool BreakUsed { get; set; }
}