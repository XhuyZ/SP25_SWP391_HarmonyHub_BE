using Domain.Entities;

namespace Repository.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Account> GetAccountByEmail(string email);
    Task<IEnumerable<Account>> GetAllTherapists();
    Task<Account> GetTherapistDetails(int therapistId);
    Task<Account> GetMemberDetails(int memberId);
}