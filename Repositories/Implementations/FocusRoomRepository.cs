using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class FocusRoomRepository : IFocusRoomRepository
{
    private readonly AppDbContext _context;
    public FocusRoomRepository(AppDbContext context) => _context = context;

    public async Task<List<FocusRoom>> GetAllAsync()
        => await _context.FocusRooms.ToListAsync(); 

    public async Task<FocusRoom?> GetByIdAsync(int id)
        => await _context.FocusRooms.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

    public async Task<FocusRoom> CreateAsync(FocusRoom room)
    {
        room.CreatedAt = DateTime.UtcNow;
        _context.FocusRooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var room = await _context.FocusRooms.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (room == null) return false;

        room.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}