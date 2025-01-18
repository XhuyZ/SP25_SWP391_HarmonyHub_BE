using System.Security.Claims;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;
using Service.Settings;

namespace Service.Implementations;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly AdminAccount _adminAccount;
    private readonly ITokenService _tokenService;
    private readonly IAccountRepository _accountRepository;

    public AccountService(IMapper mapper, IOptions<AdminAccount> adminAccount, ITokenService tokenService,
        IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _adminAccount = adminAccount.Value;
        _tokenService = tokenService;
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

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, request.Email)
        };

        if (request.Email.ToLower().Equals(_adminAccount.Email.ToLower()) &&
            request.Password.Equals(_adminAccount.Password))
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleEnum.Admin.ToString()));
            return new LoginResponse
            {
                Email = _adminAccount.Email,
                Status = (int)AccountStatusEnum.Active,
                Role = (int)RoleEnum.Admin,
                AccessToken = _tokenService.GenerateAccessToken(claims)
            };
        }

        try
        {
            var existingAccount = await _accountRepository.GetAccountByEmail(request.Email);
            if (BCrypt.Net.BCrypt.Verify(request.Password, existingAccount.HashedPassword))
            {
                claims.Add(new Claim(ClaimTypes.Role,
                    existingAccount.Role == 1 ? RoleEnum.Member.ToString() : RoleEnum.Therapist.ToString()));

                var loginResponse = _mapper.Map<LoginResponse>(existingAccount);
                loginResponse.AccessToken = _tokenService.GenerateAccessToken(claims);

                return loginResponse;
            }
        }
        catch (Exception e)
        {
            throw new ServiceException(MessageConstants.NOT_FOUND);
        }
        
        return null;
    }

    public async Task<AccountResponse> GetMemberProfile(int memberId)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(memberId);
            
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
                
            // Kiểm tra role là member
            if (account.Role != (int)RoleEnum.Member)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);

            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<AccountResponse> GetAccountByEmail(string email)
    {
        try
        {
            var account = await _accountRepository.GetAccountByEmail(email);
            
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<AccountResponse> UpdateMemberProfile(int memberId, UpdateProfileRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(memberId);
            
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
                
            if (account.Role != (int)RoleEnum.Member)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);

            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.Phone = request.Phone;
            account.Email = request.Email;
            account.Birthdate = request.Birthdate;
            account.Gender = request.Gender;
            account.AvatarUrl = request.AvatarUrl;

            await _accountRepository.UpdateAsync(account);
            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}