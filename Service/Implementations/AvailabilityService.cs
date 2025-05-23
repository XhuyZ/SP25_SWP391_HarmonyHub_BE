﻿using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilityRepository _availabilityRepository;

        public AvailabilityService(IMapper mapper, IAvailabilityRepository availabilityRepository)
        {
            _mapper = mapper;
            _availabilityRepository = availabilityRepository;
        }

        public async Task UpdateAvailability(int id, UpdateAvailabilityRequest request)
        {
            try
            {
                var check = await _availabilityRepository.GetByIdAsync(id);
                if (check != null)
                {
                    await _availabilityRepository.DeleteAsync(check);
                }
                var availability = _mapper.Map<Availability>(request);
                availability.UpdatedAt = DateTime.Now;
                availability.CreatedAt = DateTime.Now;

                await _availabilityRepository.AddAsync(availability);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task DeleteAvailability(int id)
        {
            try
            {
                var check = await _availabilityRepository.GetByIdAsync(id);
                if (check == null)
                {
                    throw new ServiceException($"Availability with ID {id} does not exist.");
                }

                await _availabilityRepository.DeleteAsync(check);
            }
            catch (Exception e)
            {
                throw new ServiceException($"An error occurred while deleting availability: {e.Message}");
            }
        }

        public async Task CreateAvailability(CreateAvailabilityRequest request)
        {
            try
            {
                var availability = _mapper.Map<Availability>(request);

                await _availabilityRepository.AddAsync(availability);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
    }
}