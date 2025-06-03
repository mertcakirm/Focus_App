using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface IPomodoroRepository
{
    Task<List<PomodoroSession>> GetUserSessionsAsync(int userId);
    Task<PomodoroSession> StartSessionAsync(PomodoroSession session);
    Task<PomodoroSession?> EndSessionAsync(int id, int userId);
}