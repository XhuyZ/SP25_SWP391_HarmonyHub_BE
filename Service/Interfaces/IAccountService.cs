using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountResponse>> GetAllAccounts();
    Task<AccountResponse> GetAccountById(int accountId);
    Task CreateAccount(CreateAccountRequest request);
    Task<string> Login(LoginRequest request);
}