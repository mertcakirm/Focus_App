using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class PomodoroRepository : IPomodoroRepository
{
    private readonly AppDbContext _context;
    public PomodoroRepository(AppDbContext context) => _context = context;

    public async Task<List<PomodoroSession>> GetUserSessionsAsync(int userId)
    {
        return await _context.PomodoroSessions.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<PomodoroSession> StartSessionAsync(PomodoroSession session)
    {
        session.StartTime = DateTime.UtcNow;
        _context.PomodoroSessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<PomodoroSession?> EndSessionAsync(int id, int userId)
    {
        var session = await _context.PomodoroSessions.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (session == null) return null;

        session.EndTime = DateTime.UtcNow;
        session.DurationMinutes = (int)(session.EndTime - session.StartTime).TotalMinutes;
        await _context.SaveChangesAsync();

        return session;
    }
}