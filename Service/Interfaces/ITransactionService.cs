using Domain.DTOs.Responses;
using Domain.Entities;

namespace Service.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponse>> GetAllTransactions();
    Task<TransactionResponse> GetTransactionByTransactionId(string transactionId);
    Task CreateTransaction(Transaction transaction);
    Task UpdateTransactionStatus(string transactionId, int status);
}