namespace Domain.Entities;

public class Result: BaseEntity
{
    public string Content { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
}