namespace Domain.Entities;

public class Appointment: BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? MeetUrl { get; set; }
    public string ClientNote { get; set; }
    public string? TherapistNote { get; set; }
    public double? FeedbackRating { get; set; }
    public string? FeedbackContent { get; set; }
    public DateTime? FeedbackDate { get; set; }
    public int Status { get; set; }
    public int? MemberId { get; set; }
    public Account? Member { get; set; }
    public int? TherapistId { get; set; }
    public Account? Therapist { get; set; }
    public int PackageId { get; set; }
    public Package Package { get; set; }
    
    public ICollection<Transaction>? Transactions { get; set; }
}