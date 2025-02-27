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

    }
}
