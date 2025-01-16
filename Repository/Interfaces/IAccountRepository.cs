using Domain.Entities;

namespace Repository.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Account> GetAccountByEmail(string email);
}