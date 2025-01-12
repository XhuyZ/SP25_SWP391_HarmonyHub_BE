namespace Domain.Entities;

public class Question: BaseEntity
{
    public string Content { get; set; }
    
    public ICollection<QuizQuestion> QuizQuestions { get; set; }
    public ICollection<Option> Options { get; set; }
}