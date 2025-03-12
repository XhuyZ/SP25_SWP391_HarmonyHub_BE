using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface IQuizService
{
    Task<IEnumerable<QuizResponse>> GetAllQuizzes();
    Task<QuizResponse> CreateQuizAsync(CreateQuizRequest request);
    Task<bool> SetQuizStatus(int quizId, int status);
    Task<bool> DeleteQuestionAsync(int questionId);
    Task<QuizResponse> GetQuizById(int id);
    Task<QuizResponse> UpdateAvatarUrl(int id, IFormFile imgFile);
}