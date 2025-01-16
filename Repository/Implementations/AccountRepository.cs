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
            .SingleOrDefaultAsync(x => x.Email.Equals(email) && x.Role != (int)RoleEnum.System);
    }
}