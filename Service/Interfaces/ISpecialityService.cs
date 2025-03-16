using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces;

public interface ISpecialtyService
{
    Task<IEnumerable<SpecialtyResponse>> GetAllSpecialties();
    Task<SpecialtyResponse> GetSpecialtyByID(int id);
    Task CreateSpecialty(CreateSpecialtyRequest request);
}
