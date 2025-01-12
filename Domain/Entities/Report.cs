namespace Domain.Entities;

public class Report: BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}