using AutoMapper;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }
}