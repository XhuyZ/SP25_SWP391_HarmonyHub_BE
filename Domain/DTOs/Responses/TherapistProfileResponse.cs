namespace Domain.DTOs.Responses;

public class TherapistProfileResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public int YearsOfExperience { get; set; }
    public string Bio { get; set; }
}