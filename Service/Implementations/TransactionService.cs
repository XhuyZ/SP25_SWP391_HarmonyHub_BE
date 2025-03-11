using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
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

    public async Task<IEnumerable<TransactionResponse>> GetAllTransactions()
    {
        try
        {
            var result = await _transactionRepository.GetAllTransactions();
            return _mapper.Map<IEnumerable<TransactionResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<TransactionResponse> GetTransactionByTransactionId(string transactionId)
    {
        try
        {
            var result = await _transactionRepository.GetTransactionByTransactionId(transactionId);
            if (result == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<TransactionResponse>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task CreateTransaction(Transaction transaction)
    {
        try
        {
            await _transactionRepository.AddAsync(transaction);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateTransactionStatus(string transactionId, int status)
    {
        try
        {
            var existingTransaction = await _transactionRepository.GetTransactionByTransactionId(transactionId);
            existingTransaction.Status = status;
            await _transactionRepository.UpdateAsync(existingTransaction);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}