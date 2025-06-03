namespace Focus_App.Models;
public class FocusRoom
{
    public int Id { get; set; }
    public string RoomName { get; set; }
    public bool IsPrivate { get; set; }
    public int MaxUsers { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<RoomParticipant> Participants { get; set; }
}