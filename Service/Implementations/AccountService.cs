using System.Security.Claims;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repository.Implementations;
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
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IQualificationRepository _qualificationRepository;

    public AccountService(IMapper mapper, IOptions<AdminAccount> adminAccount, ITokenService tokenService,
        IAccountRepository accountRepository, ICloudinaryService cloudinaryService, IQualificationRepository qualificationRepository)
    {
        _mapper = mapper;
        _adminAccount = adminAccount.Value;
        _tokenService = tokenService;
        _accountRepository = accountRepository;
        _cloudinaryService = cloudinaryService;
        _qualificationRepository = qualificationRepository;
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

    public async Task<IEnumerable<TherapistDetailsResponse>> GetAllTherapists()
    {
        try
        {
            var result = await _accountRepository.GetAllTherapists();
            return _mapper.Map<IEnumerable<TherapistDetailsResponse>>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<TherapistDetailsResponse> GetTherapistDetails(int therapistId)
    {
        try
        {
            var result = await _accountRepository.GetTherapistDetails(therapistId);
            if (result == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<TherapistDetailsResponse>(result);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<MemberDetailsResponse> GetMemberDetails(int memberId)
    {
        try
        {
            var result = await _accountRepository.GetMemberDetails(memberId);
            if (result == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            return _mapper.Map<MemberDetailsResponse>(result);
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

    public async Task RegisterMember(RegisterMemberRequest request)
    {
        try
        {
            var existingAccount = await _accountRepository.GetAccountByEmail(request.Email);
            if (existingAccount != null)
                throw new ServiceException(MessageConstants.DUPLICATE);

            var account = _mapper.Map<Account>(request);
            account.Balance = 0;
            account.Role = (int)RoleEnum.Member;
            account.Status = (int)AccountStatusEnum.Active;

            await _accountRepository.AddAsync(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task RegisterTherapist(RegisterTherapistRequest request)
    {
        try
        {
            var existingAccount = await _accountRepository.GetAccountByEmail(request.Email);
            if (existingAccount != null)
                throw new ServiceException(MessageConstants.DUPLICATE);

            var account = _mapper.Map<Account>(request);
            account.Balance = 0;
            account.Role = (int)RoleEnum.Therapist;
            account.Status = (int)AccountStatusEnum.Pending;

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
                claims.Add(new Claim("AccountId", existingAccount.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role,
                    existingAccount.Role == (int)RoleEnum.Member
                        ? RoleEnum.Member.ToString()
                        : RoleEnum.Therapist.ToString()));

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

    public async Task UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(memberId);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            account.RelationshipGoal = request.RelationshipGoal;
            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.Birthdate = request.Birthdate;
            account.Gender = request.Gender;
            account.Bio = request.Bio;
            
            await _accountRepository.UpdateAsync(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateTherapistInfo(int therapistId, UpdateTherapistInfoRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(therapistId);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.Birthdate = request.Birthdate;
            account.Gender = request.Gender;
            account.YearsOfExperience = request.YearsOfExperience;
            account.Bio = request.Bio;

            await _accountRepository.UpdateAsync(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task UpdateTherapistQualification(int qualificationId,
        UpdateTherapistQualificationRequest request)
    {
        try
        {
            var qualification = await _qualificationRepository.GetByIdAsync(qualificationId);
            if (qualification == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            qualification.Degree = request.Degree;
            qualification.ImageUrl = request.ImageUrl;

            await _qualificationRepository.UpdateAsync(qualification);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<AccountResponse> UpdateAvatarUrl(int id, IFormFile avatarFile)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            var avatarUrl = await _cloudinaryService.UploadFile(avatarFile);

            account.AvatarUrl = avatarUrl;
            await _accountRepository.UpdateAsync(account);

            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<bool> UpdateAccountStatus(int id, int status) 
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account.Status == (int)status)
                throw new ServiceException($"Account is already {status}.");
            if (account == null)
                throw new ServiceException("Account not found.");
            account.Status = status;
            await _accountRepository.UpdateAsync(account);
            return true;
        }
        catch (Exception e)
        {
            throw new ServiceException($"Error updating account status: {e.Message}", e);
        }
    }
}