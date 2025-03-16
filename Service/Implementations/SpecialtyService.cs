using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class SpecialtyService : ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;
    private readonly IMapper _mapper;

    public SpecialtyService(ISpecialtyRepository specialtyRepository, IMapper mapper)
    {
        _specialtyRepository = specialtyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SpecialtyResponse>> GetAllSpecialties()
    {
        try 
        {
            var specialties = await _specialtyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpecialtyResponse>>(specialties);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task<SpecialtyResponse> GetSpecialtyByID(int id)
    {
        try
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
                throw new ServiceException("Specialty not found");
            
            return _mapper.Map<SpecialtyResponse>(specialty);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task CreateSpecialty(CreateSpecialtyRequest request)
    {
        try
        {
            var specialty = _mapper.Map<Specialty>(request);
            specialty.CreatedAt = DateTime.Now;
            await _specialtyRepository.AddAsync(specialty);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<bool> UpdateSpecialty(int id, UpdateSpecialtyRequest request)
    {
        try
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
                throw new ServiceException("Specialty not found.");

            _mapper.Map(request, specialty);
            specialty.UpdatedAt = DateTime.Now;
            await _specialtyRepository.UpdateAsync(specialty);
            return true;
        }
        catch (Exception e)
        {
            throw new ServiceException($"Error updating specialty : {e.Message}", e);
        }
    }
}
