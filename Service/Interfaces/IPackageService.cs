using Domain.DTOs.Requests;

namespace Service.Interfaces;

public interface IPackageService
{
    Task CreatePackage(int therapistId, CreatePackageRequest request);
    Task ChangePackageStatus(int packageId, ChangePackageStatusRequest request);
}