namespace Domain.DTOs.Responses;

public class AvailabilityResponse
{
    public int DayOfWeek { get; set; }
    public TimeOnly FromTime { get; set; }
    public TimeOnly ToTime { get; set; }
}