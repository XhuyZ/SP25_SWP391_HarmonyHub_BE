namespace Domain.Entities;

public class Quiz: BaseEntity
{
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
    public int TherapistId { get; set; }
    public Account Therapist { get; set; }
    
    public ICollection<QuizQuestion> QuizQuestions { get; set; }
    public ICollection<Result> Results { get; set; }
}