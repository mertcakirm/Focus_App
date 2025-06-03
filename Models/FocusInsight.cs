namespace Focus_App.Models;
public class FocusInsight
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public int TotalMinutes { get; set; }
    public string SuggestedTip { get; set; }
    public bool IsDeleted { get; set; } = false;
    public User User { get; set; }
}