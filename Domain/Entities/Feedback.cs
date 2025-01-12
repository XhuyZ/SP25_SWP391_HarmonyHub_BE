namespace Domain.Entities;

public class Feedback: BaseEntity
{
    public double Rating { get; set; }
    public string Content { get; set; }
    public int? MemberId { get; set; }
    public Account? Member { get; set; }
    public int? PackageId { get; set; }
    public Package Package { get; set; }
}