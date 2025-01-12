namespace Domain.Entities;

public class Option: BaseEntity
{
    public string Content { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}