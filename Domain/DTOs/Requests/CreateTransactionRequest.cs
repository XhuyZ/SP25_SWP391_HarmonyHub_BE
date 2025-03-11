namespace Domain.DTOs.Requests;

public class CreateTransactionRequest
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int AppointmentId { get; set; }
}