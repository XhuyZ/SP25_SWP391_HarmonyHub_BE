using Domain.Entities;

namespace Domain.DTOs.Requests
{
    public class CreateQuizQuestionRequest
    {
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
