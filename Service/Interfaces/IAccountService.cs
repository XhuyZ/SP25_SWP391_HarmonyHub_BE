using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountResponse>> GetAllAccounts();
    Task<AccountResponse> GetAccountById(int accountId);
    Task CreateAccount(CreateAccountRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<MemberProfileResponse> GetMemberProfile(int memberId);
    Task<AccountResponse> GetAccountByEmail(string email);
    Task<MemberProfileResponse> UpdateMemberProfile(int memberId, UpdateProfileRequest request);
    Task<TherapistProfileResponse> GetTherapistProfile(int therapistId);
    Task<TherapistProfileResponse> UpdateTherapistProfile(int therapistId, UpdateTherapistProfileRequest request);

}