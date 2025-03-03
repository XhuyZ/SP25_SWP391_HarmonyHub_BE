using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizResponse>> GetAllQuizzes();
        Task<QuizResponse> CreateQuizAsync(CreateQuizRequest request);
        Task<bool> SetQuizStatus(int quizId, int status);
        Task<bool> DeleteQuestionAsync(int questionId);
        Task<QuizResponse> GetQuizById(int id);

    }
}
