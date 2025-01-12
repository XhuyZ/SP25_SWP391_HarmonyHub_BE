namespace Domain.Entities;

public class Availability: BaseEntity
{
    public int DayOfWeek { get; set; }
    public TimeOnly FromTime { get; set; }
    public TimeOnly ToTime { get; set; }
    public int TherapistId { get; set; }
    public Account Therapist { get; set; }
}