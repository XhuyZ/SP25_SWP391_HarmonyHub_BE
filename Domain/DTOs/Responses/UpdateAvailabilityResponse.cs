namespace Domain.DTOs.Responses;

public class UpdateAvailabilityResponse
{
    public int AvailabilityId { get; set; }
    public TimeOnly FromTime { get; set; }
    public TimeOnly ToTime { get; set; }
    public int TherapistId { get; set; }
}