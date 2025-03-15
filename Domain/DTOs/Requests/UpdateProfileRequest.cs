public class UpdateProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public string? Bio { get; set; }
    public string? RelationshipGoal { get; set; }
} 