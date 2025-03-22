namespace Domain.DTOs.Requests;

public class CreateQuizRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int TherapistId { get; set; }
    public List<QuestionRequest> Questions { get; set; }
    public List<ResultRequest> Results { get; set; }
}