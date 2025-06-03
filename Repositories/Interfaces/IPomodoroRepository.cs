using Focus_App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Focus_App.Repositories.Interfaces
{
    public interface IPomodoroSessionRepository
    {
		Task<PomodoroSession> StartSessionAsync(PomodoroSession session);
		Task<PomodoroSession?> EndSessionAsync(int sessionId, int userId);
		Task<List<PomodoroSession>> GetUserSessionsAsync(int userId);
    }
}