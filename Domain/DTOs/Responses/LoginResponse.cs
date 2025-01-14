namespace Domain.DTOs.Responses;

public class LoginResponse
{
    public int AccountId { get; set; }
    public string AvatarUrl { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Status { get; set; }
    public int Role { get; set; }
    public string AccessToken { get; set; }
}