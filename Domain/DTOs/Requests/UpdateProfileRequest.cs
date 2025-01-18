public class UpdateProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public string AvatarUrl { get; set; }
} 