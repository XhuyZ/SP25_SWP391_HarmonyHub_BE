namespace Domain.DTOs.Responses;

public class TransactionResponse
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethod { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
    public int? SenderId { get; set; }
    public string SenderFullName { get; set; }
    public int? ReceiverId { get; set; }
    public string ReceiverFullName { get; set; }
    public int? AppointmentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}