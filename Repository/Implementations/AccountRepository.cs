using Domain.Entities;
using Repository.Interfaces;

namespace Repository.Implementations;

public class AccountRepository : GenericRepository<Account>, IAccountRepository;