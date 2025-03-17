namespace Domain.Entities;

public class Result: BaseEntity
{
    public int Type { get; set; }
    public string Content { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
}