using System.Security.Claims;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
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

    public AccountService(IMapper mapper, IOptions<AdminAccount> adminAccount, ITokenService tokenService,
        IAccountRepository accountRepository, ICloudinaryService cloudinaryService)
    {
        _mapper = mapper;
        _adminAccount = adminAccount.Value;
        _tokenService = tokenService;
        _accountRepository = accountRepository;
        _cloudinaryService = cloudinaryService;
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

    public async Task<AccountResponse> UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(memberId);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
            if (account.Role != (int)RoleEnum.Member)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);
            account.Email = request.Email;
            account.Phone = request.Phone;
            account.RelationshipGoal = request.RelationshipGoal;
            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.Birthdate = request.Birthdate;
            account.Gender = request.Gender;
            account.Bio = request.Bio;
            await _accountRepository.UpdateAsync(account);
            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task<AccountResponse> UpdateTherapistInfo(int therapistId, UpdateTherapistInfoRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(therapistId);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
            if (account.Role != (int)RoleEnum.Therapist)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);
            account.Email = request.Email;
            account.Phone = request.Phone;
            account.FirstName = request.FirstName;
            account.LastName = request.LastName;
            account.Birthdate = request.Birthdate;
            account.Gender = request.Gender;
            account.YearsOfExperience = request.YearsOfExperience;
            account.Bio = request.Bio;
            await _accountRepository.UpdateAsync(account);
            return _mapper.Map<AccountResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task<TherapistQualificationResponse> GetTherapistQualification(int therapistId) 
    {
        try
        {
            var account = await _accountRepository.GetTherapistDetails(therapistId);

            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);

            if (account.Role != (int)RoleEnum.Therapist)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);

            return _mapper.Map<TherapistQualificationResponse>(account);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
    public async Task<TherapistQualificationResponse> UpdateTherapistQualification(int therapistId, UpdateTherapistQualificationRequest request)
    {
        try
        {
            var account = await _accountRepository.GetByIdAsync(therapistId);
            if (account == null)
                throw new ServiceException(MessageConstants.NOT_FOUND);
            if (account.Role != (int)RoleEnum.Therapist)
                throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);

            account.FirstName = account.FirstName;
            account.LastName = account.LastName;
            account.YearsOfExperience = account.YearsOfExperience;
            if (request.Qualifications != null)
            {
                var qualifications = new List<Qualification>();
                foreach (var qualification in request.Qualifications)
                {
                    qualifications.Add(new Qualification
                    {
                        Degree = qualification.Degree,
                        ImageUrl = qualification.ImageUrl,
                        TherapistId = therapistId,
                        SpecialtyId = qualification.SpecialtyId
                    });
                }
                account.Qualifications = qualifications; // Cập nhật danh sách Qualifications
            }
            await _accountRepository.UpdateAsync(account);
            return _mapper.Map<TherapistQualificationResponse>(account);
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
}