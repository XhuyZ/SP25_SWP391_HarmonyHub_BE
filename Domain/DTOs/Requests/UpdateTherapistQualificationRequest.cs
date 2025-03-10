using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Responses;
using Domain.Entities;

namespace Domain.DTOs.Requests;
public class UpdateTherapistQualificationRequest
{
    public int? YearsOfExperience { get; set; }
    public List<Qualification> Qualifications { get; set; }
}
