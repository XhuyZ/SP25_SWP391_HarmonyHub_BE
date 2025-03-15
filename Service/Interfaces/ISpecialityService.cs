using Domain.DTOs.Responses;

namespace Service.Interfaces;

public interface ISpecialtyService
{
    Task<IEnumerable<SpecialtyResponse>> GetAllSpecialties();
    Task<SpecialtyResponse> GetSpecialtyByID(int id);
}
