using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Implementations;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository;