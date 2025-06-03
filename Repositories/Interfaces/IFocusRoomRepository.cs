using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface IFocusRoomRepository
{
    Task<List<FocusRoom>> GetAllAsync();
    Task<FocusRoom?> GetByIdAsync(int id);
    Task<FocusRoom> CreateAsync(FocusRoom room);
    Task<bool> DeleteAsync(int id);
}