namespace Domain.DTOs.Requests;

public class CreateAppointmentRequest
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ClientNote { get; set; }
    public int TherapistId { get; set; }
    public int PackageId { get; set; }
}