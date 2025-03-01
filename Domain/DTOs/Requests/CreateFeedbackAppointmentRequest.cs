namespace Domain.DTOs.Requests
{
    public class CreateFeedbackAppointmentRequest
    {
        public double? FeedbackRating { get; set; }
        public string? FeedbackContent { get; set; }
        public DateTime? FeedbackDate { get; set; }
    }
}
