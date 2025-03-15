using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses;

public class TherapistQualificationResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int YearsOfExperience { get; set; }
    public List<QualificationResponses> Qualifications { get; set; }
}
