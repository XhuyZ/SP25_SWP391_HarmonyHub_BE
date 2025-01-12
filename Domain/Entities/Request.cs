namespace Domain.Entities;

public class Request: BaseEntity
{
    public string? Description { get; set; }
    public int Status { get; set; }
    public int MemberId { get; set; }
    public Account Member { get; set; }
    
    public ICollection<PackageRequest> PackageRequests { get; set; }
}