using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class PackageService : IPackageService
{
    private readonly IMapper _mapper;
    private readonly IPackageRepository _packageRepository;

    public PackageService(IMapper mapper, IPackageRepository packageRepository)
    {
        _mapper = mapper;
        _packageRepository = packageRepository;
    }

    public async Task CreatePackage(int therapistId, CreatePackageRequest request)
    {
        var package = _mapper.Map<Package>(request);
        package.TherapistId = therapistId;
        package.Status = (int)PackageStatusEnum.Pending;

        try
        {
            await _packageRepository.AddAsync(package);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task ChangePackageStatus(int packageId, ChangePackageStatusRequest request)
    {
        var existingPackage = await _packageRepository.GetByIdAsync(packageId);
        if (existingPackage == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            existingPackage.Status = request.Status;
            await _packageRepository.UpdateAsync(existingPackage);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}