using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public async Task<Account> GetAccountByEmail(string email)
    {
        return await _context.Accounts
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Email.Equals(email.ToLower()) && x.Role != (int)RoleEnum.System);
    }

    public async Task<IEnumerable<Account>> GetAllTherapists()
    {
        return await _context.Accounts
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Availabilities)
            .Include(x => x.Packages)
            .Include(x => x.Qualifications)
            .ThenInclude(x => x.Specialty)
            .Where(x => x.Role == (int)RoleEnum.Therapist)
            .ToListAsync();
    }

    public async Task<Account> GetTherapistDetails(int therapistId)
    {
        return await _context.Accounts
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Availabilities)
            .Include(x => x.Packages)
            .Include(x => x.Qualifications)
            .ThenInclude(x => x.Specialty)
            .SingleOrDefaultAsync(x => x.Id == therapistId && x.Role == (int)RoleEnum.Therapist);
    }

    public async Task<Account> GetMemberDetails(int memberId)
    {
        return await _context.Accounts
            .SingleOrDefaultAsync(x => x.Id == memberId && x.Role == (int)RoleEnum.Member);
    }
}