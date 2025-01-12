namespace Domain.Entities;

public class PackageRequest
{
    public int RequestId { get; set; }
    public Request Request { get; set; }
    public int PackageId { get; set; } 
    public Package Package { get; set; }
}