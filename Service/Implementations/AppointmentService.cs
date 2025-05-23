﻿using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly IMapper _mapper;
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository)
    {
        _mapper = mapper;
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<AppointmentResponse>> GetMemberAppointments(int memberId)
    {
        try
        {
            var result = await _appointmentRepository.GetMemberAppointments(memberId);

            return _mapper.Map<IEnumerable<AppointmentResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<IEnumerable<AppointmentResponse>> GetTherapistAppointments(int therapistId)
    {
        try
        {
            var result = await _appointmentRepository.GetTherapistAppointments(therapistId);

            return _mapper.Map<IEnumerable<AppointmentResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<AppointmentResponse> GetAppointmentById(int id)
    {
        try
        {
            var result = await _appointmentRepository.GetAppointmentById(id);
            return _mapper.Map<AppointmentResponse>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task CreateAppointment(int memberId, CreateAppointmentRequest request)
    {
        try
        {
            var appointment = _mapper.Map<Appointment>(request);
            appointment.MemberId = memberId;
            appointment.Status = (int)AppointmentStatusEnum.Pending;
            appointment.MeetUrl = "";

            await _appointmentRepository.AddAsync(appointment);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task ChangeAppointmentStatus(int appointmentId, ChangeAppointmentStatusRequest request)
    {
        var existingAppointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (existingAppointment == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);

        try
        {
            existingAppointment.Status = request.Status;
            await _appointmentRepository.UpdateAsync(existingAppointment);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateFeedbackAppointment(int appointmentId, UpdateFeedbackAppointmentRequest request)
    {
        var existingFeedback = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (existingFeedback == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            existingFeedback.FeedbackRating = request.FeedbackRating;
            existingFeedback.FeedbackContent = request.FeedbackContent;
            existingFeedback.FeedbackDate = DateTime.Now;
            await _appointmentRepository.UpdateAsync(existingFeedback);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    
    public async Task DeleteFeedbackAppointment(int appointmentId) 
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            appointment.FeedbackRating = null;
            appointment.FeedbackContent = null;
            appointment.FeedbackDate = null;

            await _appointmentRepository.UpdateAsync(appointment);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateAppointmentNote(int appointmentId, UpdateTherapistAppointmentRequest request)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            appointment.TherapistNote = request.TherapistNote;
            appointment.UpdatedAt = DateTime.Now;
            await _appointmentRepository.UpdateAsync(appointment);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateAppointmentMeetUrl(int appointmentId, UpdateMeetingUrlRequest request)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            appointment.MeetUrl = request.MeetingUrl;
            appointment.UpdatedAt = DateTime.Now;
            await _appointmentRepository.UpdateAsync(appointment);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}