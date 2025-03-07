using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountResponse>> GetAllAccounts();
    Task<IEnumerable<TherapistDetailsResponse>> GetAllTherapists();
    Task<TherapistDetailsResponse> GetTherapistDetails(int therapistId);
    Task<AccountResponse> GetAccountById(int accountId);
    Task RegisterMember(RegisterMemberRequest request);
    Task RegisterTherapist(RegisterTherapistRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<MemberProfileResponse> GetMemberProfile(int memberId);
    Task<AccountResponse> GetAccountByEmail(string email);
    Task<MemberProfileResponse> UpdateMemberProfile(int memberId, UpdateProfileRequest request);
    Task<TherapistProfileResponse> GetTherapistProfile(int therapistId);
    Task<TherapistProfileResponse> UpdateTherapistProfile(int therapistId, UpdateTherapistProfileRequest request);
    Task<AccountResponse> UpdateAvatarUrl(int id, IFormFile avatarFile);
    Task<AccountResponse> UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request);

}