namespace Domain.Entities;

public class Option: BaseEntity
{
    public int Type { get; set; }
    public string Content { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}