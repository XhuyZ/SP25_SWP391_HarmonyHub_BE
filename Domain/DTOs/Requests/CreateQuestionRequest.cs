using Domain.Entities;

namespace Domain.DTOs.Requests
{
    public class CreateQuestionRequest
    {
        public string Content { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
