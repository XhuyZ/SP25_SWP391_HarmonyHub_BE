namespace Domain.Entities;

public class Package: BaseEntity
{
    public int Type { get; set; }
    public string Description { get; set; }
    public int NumberOfSessions { get; set; }
    public int MinutesPerSession { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    public int TherapistId { get; set; }
    public Account Therapist { get; set; }
    
    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<PackageRequest>? PackageRequests { get; set; }
}