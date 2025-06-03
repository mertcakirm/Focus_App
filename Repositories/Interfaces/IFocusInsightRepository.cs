using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface IFocusInsightRepository
{
    Task<FocusInsight> GetOrCreateCurrentWeekAsync(int userId);
    Task<FocusInsight> UpdateMinutesAsync(int userId, int minutes);
}