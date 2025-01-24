namespace Domain.DTOs.Responses;

public class PackageResponse
{
    public string Description { get; set; }
    public int MinutesPerAppointment { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
}