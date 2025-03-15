namespace Domain.DTOs.Requests;
public class UpdateMemberInfoRequest
{
    public string? RelationshipGoal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public string? Bio { get; set; }
}
