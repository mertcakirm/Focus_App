namespace Focus_App.Models;
public class RoomParticipant
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinTime { get; set; }
    public bool IsActive { get; set; }

    public FocusRoom Room { get; set; }
    public User User { get; set; }
}