namespace Domain.DTOs.Requests
{
    public class CreateQuestionRequest
    {
        public string Content { get; set; }

        public List<OptionRequest> Options { get; set; }
    }
}
