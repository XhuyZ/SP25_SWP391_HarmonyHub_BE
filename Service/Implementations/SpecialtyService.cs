using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTOs.Responses;
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
}
