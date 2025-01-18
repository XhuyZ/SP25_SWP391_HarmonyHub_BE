using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Data;

namespace Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackResponse>> GetAllFeedbacks();
        Task<FeedbackResponse> GetFeedbackByMemberId(int id);
        Task CreateFeedback(CreateFeedbackRequest request);
        Task<Feedback> DeleteFeedbackByMemberAndPackage(int memberId, int packageId);
    }
}
