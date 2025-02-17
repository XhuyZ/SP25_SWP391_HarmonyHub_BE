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

    public async Task CreateAppointment(int memberId, CreateAppointmentRequest request)
    {
        try
        {
            var appointment = _mapper.Map<Appointment>(request);
            appointment.MemberId = memberId;
            appointment.Status = (int)AppointmentStatusEnum.Pending;

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
    public async Task<IEnumerable<AppointmentFeedbackResponse>> GetAllAppointmentFeedback()
    {
        try
        {
            var feedback = await _appointmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentFeedbackResponse>>(feedback);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task<AppointmentFeedbackResponse> GetAppointmentFeedbackID(int appointmentId)
    {
        try
        {
            var feedback = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (feedback == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
            return _mapper.Map<AppointmentFeedbackResponse>(feedback);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task CreateFeedbackAppointment(int appointmentId, CreateFeedbackAppointmentRequest request)
    {
        var apointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (apointment == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            apointment.FeedbackRating = request.FeedbackRating;
            apointment.FeedbackContent = request.FeedbackContent;
            apointment.FeedbackDate = DateTime.Now;

            await _appointmentRepository.UpdateAsync(apointment);
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
}