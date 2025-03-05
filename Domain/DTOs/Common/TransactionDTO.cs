using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTOs.Common
{
    public class TransactionDTO
    {
        public int UserId { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethod { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
