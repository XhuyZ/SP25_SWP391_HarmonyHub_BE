namespace Domain.DTOs.Requests
{
    public class CreateAvailabilityRequest
    {
        public int DayOfWeek { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }
        public int TherapistId { get; set; }
    }
}
