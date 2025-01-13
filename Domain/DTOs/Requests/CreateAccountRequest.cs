namespace Domain.DTOs.Requests;

public class CreateAccountRequest
{
    public string? AvatarUrl { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string? RelationshipGoal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Bio { get; set; }
    public decimal Balance { get; set; }
    public int Status { get; set; }
    public int Role { get; set; } 
}