namespace Domain.Entities;

public class Blog: BaseEntity
{
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public int? TherapistId { get; set; }
    public Account Therapist { get; set; }
}