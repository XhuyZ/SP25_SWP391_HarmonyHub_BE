using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class VnpayPaymentResponse
    {
        public string PaymentId { get; set; }             // vnp_TxnRef
        public decimal Amount { get; set; }               // vnp_Amount
        public string BankCode { get; set; }              // vnp_BankCode
        public string BankTransactionNo { get; set; }     // vnp_BankTranNo
        public string CardType { get; set; }              // vnp_CardType
        public string OrderInfo { get; set; }             // vnp_OrderInfo
        public DateTime PaymentDate { get; set; }         // vnp_PayDate
        public string TransactionNo { get; set; }         // vnp_TransactionNo
        public string TransactionStatus { get; set; }     // vnp_TransactionStatus
        public bool PaymentStatus { get; set; }           // Dựa vào vnp_ResponseCode & vnp_TransactionStatus
        public string PaymentStatusMessage { get; set; }  // Thông báo trạng thái
    }
}
