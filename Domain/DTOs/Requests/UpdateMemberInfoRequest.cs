using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests;

public class UpdateMemberInfoRequest
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? RelationshipGoal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Bio { get; set; }
}
