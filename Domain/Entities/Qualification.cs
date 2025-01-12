namespace Domain.Entities;

public class Qualification: BaseEntity
{
    public int Degree { get; set; }
    public string ImageUrl { get; set; }
    public int SpecialtyId { get; set; }
    public Specialty Specialty { get; set; }
    public int TherapistId { get; set; }
    public Account Therapist { get; set; }
}