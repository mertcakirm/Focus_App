public class FocusRoomCreateDto
{
    public string RoomName { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public int MaxUsers { get; set; }
}