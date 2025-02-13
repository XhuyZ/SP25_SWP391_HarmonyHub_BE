using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetMemberAppointments(int memberId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Member)
            .Include(x => x.Therapist)
            .Include(x => x.Package)
            .Where(x => x.MemberId == memberId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetTherapistAppointments(int therapistId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Member)
            .Include(x => x.Therapist)
            .Include(x => x.Package)
            .Where(x => x.TherapistId == therapistId)
            .ToListAsync();
    }
}