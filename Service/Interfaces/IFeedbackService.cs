using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackResponse>> GetAllFeedbacks();
        Task<FeedbackResponse> GetFeedbackByMemberId(int id);
        Task CreateFeedback(CreateFeedbackRequest request);
        
    }
}
