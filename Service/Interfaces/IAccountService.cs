using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

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
    Task<AccountResponse> GetMemberProfile(int memberId);
    Task<AccountResponse> UpdateMemberProfile(int memberId, UpdateProfileRequest request);
}