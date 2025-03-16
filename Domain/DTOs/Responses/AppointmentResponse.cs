namespace Domain.DTOs.Responses;

public class AppointmentResponse
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MeetUrl { get; set; }
    public string ClientNote { get; set; }
    public string? TherapistNote { get; set; }
    public double? FeedbackRating { get; set; }
    public string? FeedbackContent { get; set; }
    public DateTime? FeedbackDate { get; set; }
    public int Status { get; set; }
    public int MemberId { get; set; }
    public string MemberFullName { get; set; }
    public int? TherapistId { get; set; }
    public string TherapistFullName { get; set; }
    public int PackageId { get; set; }
    public string PackageName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}