using Domain.DTOs.Requests;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class PackageService : IPackageService
{
    private readonly IPackageRepository _packageRepository;

    public PackageService(IPackageRepository packageRepository)
    {
        _packageRepository = packageRepository;
    }

    public Task CreatePackage(int therapistId, CreatePackageRequest request)
    {
        throw new NotImplementedException();
    }

    public Task ChangePackageStatus(int packageId)
    {
        throw new NotImplementedException();
    }
}