using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class CreatePaymentResponse
    {
        public string OrderId { get; set; }

        public string OrderInfo { get; set; }

        public string Amount { get; set; }

        public string PayDate { get; set; }
    }
}
