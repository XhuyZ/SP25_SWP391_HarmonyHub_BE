namespace Domain.DTOs.Requests;

public class AddQualificationRequest
{
    public string Email { get; set; }
    public int Degree { get; set; }
    public string ImageUrl { get; set; }
    public int SpecialtyId { get; set; }
}