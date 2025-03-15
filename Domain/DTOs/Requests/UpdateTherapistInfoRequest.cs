namespace Domain.DTOs.Requests;
public class UpdateTherapistInfoRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Bio { get; set; }
}
