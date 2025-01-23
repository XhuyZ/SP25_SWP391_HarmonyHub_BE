using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Implementations;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository;