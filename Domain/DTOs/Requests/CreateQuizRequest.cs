using Domain.Entities;

namespace Domain.DTOs.Requests
{
    public class CreateQuizRequest
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int TherapistId { get; set; }

        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
