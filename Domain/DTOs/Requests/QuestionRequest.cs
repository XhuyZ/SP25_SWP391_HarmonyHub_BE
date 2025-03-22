namespace Domain.DTOs.Requests
{
    public class QuestionRequest
    {
        public string Content { get; set; }
        public List<OptionRequest> Options { get; set; }
    }
}
