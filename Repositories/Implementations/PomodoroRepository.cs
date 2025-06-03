using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations
{
    public class PomodoroSessionRepository : IPomodoroSessionRepository
    {
        private readonly AppDbContext _context;

        public PomodoroSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PomodoroSession> StartSessionAsync(PomodoroSession session)
        {
            session.StartTime = DateTime.UtcNow;
            _context.PomodoroSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<PomodoroSession?> EndSessionAsync(int sessionId, int userId)
        {
            var session = await _context.PomodoroSessions.FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);
            if (session == null) return null;

            session.EndTime = DateTime.UtcNow;
            session.DurationMinutes = (int)(session.EndTime - session.StartTime).TotalMinutes;
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<List<PomodoroSession>> GetUserSessionsAsync(int userId)
        {
            return await _context.PomodoroSessions
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    } 
}