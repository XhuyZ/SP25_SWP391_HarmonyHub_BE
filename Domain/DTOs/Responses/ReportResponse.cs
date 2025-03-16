namespace Domain.DTOs.Responses;

public class ReportResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public int? AccountId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}