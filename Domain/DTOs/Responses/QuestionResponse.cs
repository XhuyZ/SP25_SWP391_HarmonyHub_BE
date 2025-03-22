namespace Domain.DTOs.Responses;

public class QuestionResponse
{
    public int id { get; set; }
    public string Content { get; set; }
    public List<OptionResponse> OptionResponse { get; set; } = new List<OptionResponse>();
}