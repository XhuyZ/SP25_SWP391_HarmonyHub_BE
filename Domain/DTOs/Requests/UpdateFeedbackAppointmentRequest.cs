namespace Domain.DTOs.Requests;

public class UpdateFeedbackAppointmentRequest
{
    public double? FeedbackRating { get; set; }
    public string? FeedbackContent { get; set; }
}