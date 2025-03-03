using Domain.Entities;

namespace Repository.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetMemberAppointments(int memberId);
    Task<IEnumerable<Appointment>> GetTherapistAppointments(int therapistId);
    Task<Appointment> GetAppointmentById(int appointmentId);
}