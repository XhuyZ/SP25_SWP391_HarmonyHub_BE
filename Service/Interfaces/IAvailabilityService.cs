using Domain.DTOs.Requests;

namespace Service.Interfaces;

public interface IAvailabilityService
{
    Task UpdateAvailability(int id, UpdateAvailabilityRequest request);
    Task DeleteAvailability(int id);
}