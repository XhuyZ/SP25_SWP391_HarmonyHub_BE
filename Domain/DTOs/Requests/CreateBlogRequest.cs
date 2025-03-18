namespace Domain.DTOs.Requests;

public class CreateBlogRequest
{
    public string ImageUrl { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int TherapistId { get; set; }
}