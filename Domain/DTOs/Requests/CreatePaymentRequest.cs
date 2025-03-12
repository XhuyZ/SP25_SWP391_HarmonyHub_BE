namespace Domain.DTO.Request;

public class CreatePaymentRequest
{
    public long Amount { get; set; }
    public string OrderInfo { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int AppointmentId { get; set; }
}