using Domain.Entities;

namespace Repository.Interfaces;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetAllTransactions();
    Task<Transaction> GetTransactionByTransactionId(string transactionId);
}