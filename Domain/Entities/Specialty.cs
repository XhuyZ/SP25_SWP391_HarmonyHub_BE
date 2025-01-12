namespace Domain.Entities;

public class Specialty: BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public ICollection<Qualification>? Qualifications { get; set; }
}