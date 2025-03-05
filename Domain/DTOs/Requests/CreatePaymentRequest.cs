namespace Domain.DTO.Request;

public class CreatePaymentRequest
{
    public long Amount { get; set; }
    public string OrderInfo { get; set; }
}