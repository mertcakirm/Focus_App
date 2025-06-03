namespace Focus_App.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsPremium { get; set; }
    public DateTime JoinDate { get; set; }

    public ICollection<TaskItem> Tasks { get; set; }
    public ICollection<PomodoroSession> PomodoroSessions { get; set; }
}