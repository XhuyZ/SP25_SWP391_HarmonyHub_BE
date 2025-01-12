using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public AccountService(IMapper mapper, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<AccountResponse>> GetAllAccounts()
    {
        try
        {
            var result = await _accountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<AccountResponse> GetAccountById(int accountId)
    {
        try
        {
            var result = await _accountRepository.GetByIdAsync(accountId);
            if (result == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<AccountResponse>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task CreateAccount(CreateAccountRequest request)
    {
        try
        {
            var account = _mapper.Map<Account>(request);
            await _accountRepository.AddAsync(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}