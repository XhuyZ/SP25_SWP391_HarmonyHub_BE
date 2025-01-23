namespace Domain.DTOs.Responses;

public class QualificationResponse
{
    public int Degree { get; set; }
    public string ImageUrl { get; set; }
    public SpecialtyResponse Specialty { get; set; }
}