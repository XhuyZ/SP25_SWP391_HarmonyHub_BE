using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountResponse>> GetAllAccounts();
    Task<IEnumerable<TherapistDetailsResponse>> GetAllTherapists();
    Task<TherapistDetailsResponse> GetTherapistDetails(int therapistId);
    Task<MemberDetailsResponse> GetMemberDetails(int memberId);
    Task<AccountResponse> GetAccountById(int accountId);
    Task RegisterMember(RegisterMemberRequest request);
    Task RegisterTherapist(RegisterTherapistRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task AddTherapistQualification(AddQualificationRequest request);

    Task UpdateTherapistQualification(int qualificationId,
        UpdateTherapistQualificationRequest request);

    Task<AccountResponse> UpdateAvatarUrl(int id, IFormFile avatarFile);
    Task UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request);
    Task UpdateTherapistInfo(int memberId, UpdateTherapistInfoRequest request);
    Task<bool> UpdateAccountStatus(int id, int status);
}