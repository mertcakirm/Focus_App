using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface IRoomParticipantRepository
{
    Task<RoomParticipant> JoinRoomAsync(int roomId, int userId);
    Task<bool> LeaveRoomAsync(int roomId, int userId);
    Task<List<RoomParticipant>> GetActiveParticipantsAsync(int roomId);
}