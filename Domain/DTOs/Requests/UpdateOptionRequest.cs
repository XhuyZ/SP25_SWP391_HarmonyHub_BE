namespace Domain.DTOs.Requests;

public class UpdateOptionRequest
{
    public int id { get; set; }
    public int Type { get; set; }
    public string Content { get; set; }
}