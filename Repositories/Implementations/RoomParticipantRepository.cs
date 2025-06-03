using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class RoomParticipantRepository : IRoomParticipantRepository
{
    private readonly AppDbContext _context;
    public RoomParticipantRepository(AppDbContext context) => _context = context;

    public async Task<RoomParticipant> JoinRoomAsync(int roomId, int userId)
    {
        var participant = new RoomParticipant
        {
            RoomId = roomId,
            UserId = userId,
            JoinTime = DateTime.UtcNow,
            IsActive = true
        };

        _context.RoomParticipants.Add(participant);
        await _context.SaveChangesAsync();
        return participant;
    }

    public async Task<bool> LeaveRoomAsync(int roomId, int userId)
    {
        var participant = await _context.RoomParticipants
            .FirstOrDefaultAsync(p => p.RoomId == roomId && p.UserId == userId && p.IsActive);

        if (participant == null) return false;

        participant.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<RoomParticipant>> GetActiveParticipantsAsync(int roomId)
    {
        return await _context.RoomParticipants
            .Where(p => p.RoomId == roomId && p.IsActive)
            .ToListAsync();
    }
}