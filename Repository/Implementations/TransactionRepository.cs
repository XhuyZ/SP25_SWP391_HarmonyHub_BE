using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetAllTransactions()
    {
        return await _context.Transactions
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Include(x => x.Appointment)
            .ToListAsync();
    }

    public async Task<Transaction> GetTransactionByTransactionId(string transactionId)
    {
        return await _context.Transactions
            .AsSplitQuery()
            .Include(x => x.Sender)
            .Include(x => x.Receiver)
            .Include(x => x.Appointment)
            .SingleOrDefaultAsync(x => x.TransactionId == transactionId);
    }
}