namespace Domain.Entities;

public class Package: BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MinutesPerAppointment { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    public int TherapistId { get; set; }
    public Account Therapist { get; set; }
    
    public ICollection<Appointment>? Appointments { get; set; }
}