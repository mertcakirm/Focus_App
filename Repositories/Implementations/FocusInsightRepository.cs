using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class FocusInsightRepository : IFocusInsightRepository
{
    private readonly AppDbContext _context;
    public FocusInsightRepository(AppDbContext context) => _context = context;

    public async Task<FocusInsight> GetOrCreateCurrentWeekAsync(int userId)
    {
        var monday = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);
        var insight = await _context.FocusInsights.FirstOrDefaultAsync(x => x.UserId == userId && x.WeekStartDate == monday);

        if (insight == null)
        {
            insight = new FocusInsight
            {
                UserId = userId,
                WeekStartDate = monday,
                TotalMinutes = 0,
                SuggestedTip = "Daha fazla pomodoro ekleyerek üretkenliğini artır."
            };
            _context.FocusInsights.Add(insight);
            await _context.SaveChangesAsync();
        }

        return insight;
    }

    public async Task<FocusInsight> UpdateMinutesAsync(int userId, int minutes)
    {
        var monday = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 1);
        var insight = await _context.FocusInsights.FirstOrDefaultAsync(x => x.UserId == userId && x.WeekStartDate == monday);

        if (insight != null)
        {
            insight.TotalMinutes += minutes;
            await _context.SaveChangesAsync();
        }

        return insight!;
    }
}