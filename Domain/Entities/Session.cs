namespace Domain.Entities;

public class Session: BaseEntity
{
    public int DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int Type { get; set; }
    public string MeetUrl { get; set; }
    public string Location { get; set; }
    public string Note { get; set; }
    public int Status { get; set; }
    public int? MemberId { get; set; }
    public Account? Member { get; set; }
    public int? TherapistId { get; set; }
    public Account? Therapist { get; set; }
}