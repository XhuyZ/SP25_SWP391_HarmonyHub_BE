using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentResponse>> GetMemberAppointments(int memberId);
    Task<IEnumerable<AppointmentResponse>> GetTherapistAppointments(int therapistId);
    Task CreateAppointment(int memberId, CreateAppointmentRequest request);
    Task ChangeAppointmentStatus(int appointmentId, ChangeAppointmentStatusRequest request);
    Task<AppointmentResponse> GetAppointmentById(int id);
    Task<IEnumerable<AppointmentFeedbackResponse>> GetAllAppointmentFeedback();
    // Task<IEnumerable<AppointmentFeedbackResponse>> GetAppointmentFeedbackID(int appointmentId);
    Task CreateFeedbackAppointment(int appointmentId, CreateFeedbackAppointmentRequest request);
    Task UpdateFeedbackAppointment(int appointmentId, UpdateFeedbackAppointmentRequest request);
}