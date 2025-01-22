namespace Domain.Entities;

public class Transaction: BaseEntity
{
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethod { get; set; }
    public string? Description { get; set; }
    public int Status { get; set; }
    public int? SenderId { get; set; }
    public Account? Sender { get; set; }
    public int? ReceiverId { get; set; }
    public Account? Receiver { get; set; }
    public int? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
}