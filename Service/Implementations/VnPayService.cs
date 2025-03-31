using Domain.Constants;
using Domain.DTO.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Interfaces;
using Service.Settings;
using VNPAY_CS_ASPX;

namespace Service.Implementations
{
    public class VnPayService : IVnPayService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VnPaySettings _vnPaySettings;
        private readonly ITransactionService _transactionService;

        public VnPayService(IHttpContextAccessor httpContextAccessor, IOptions<VnPaySettings> vnPaySettings,
            ITransactionService transactionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnPaySettings = vnPaySettings.Value;
            _transactionService = transactionService;
        }

        public async Task<string> CreatePayment(CreatePaymentRequest createPaymentRequest)
        {
            string vnp_Returnurl = _vnPaySettings.ReturnUrl;
            string vnp_Url = _vnPaySettings.PayUrl;
            string vnp_TmnCode = _vnPaySettings.TmnCode;
            string vnp_HashSecret = _vnPaySettings.HashSecret;

            //Get payment input
            long orderId = DateTime.Now.Ticks;
            long amount = createPaymentRequest.Amount;
            DateTime createdDate = DateTime.Now;

            //Save order to db

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _vnPaySettings.Version);
            vnpay.AddRequestData("vnp_Command", _vnPaySettings.PayCommand);
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (amount * 25000 * 100).ToString());

            vnpay.AddRequestData("vnp_CreateDate", createdDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _vnPaySettings.CurrencyCode);
            vnpay.AddRequestData("vnp_IpAddr", VnPayUtils.GetIpAddress(_httpContextAccessor));

            vnpay.AddRequestData("vnp_Locale", _vnPaySettings.Locale);

            vnpay.AddRequestData("vnp_OrderInfo", createPaymentRequest.OrderInfo);
            vnpay.AddRequestData("vnp_OrderType", _vnPaySettings.OrderType);

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

            var transaction = new Transaction
            {
                Amount = amount,
                Description = createPaymentRequest.OrderInfo,
                PaymentMethod = (int)PaymentMethodEnum.VnPay,
                Status = (int)TransactionStatusEnum.Created,
                TransactionId = orderId.ToString(),
                SenderId = createPaymentRequest.SenderId,
                ReceiverId = createPaymentRequest.ReceiverId,
                AppointmentId = createPaymentRequest.AppointmentId
            };
            await _transactionService.CreateTransaction(transaction);

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return paymentUrl;
        }

        public async Task<int> GetPaymentResult()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context.Request.Query.Count > 0)
            {
                string vnp_HashSecret = _vnPaySettings.HashSecret;
                var vnpayData = context.Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }

                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = context.Request.Query["vnp_SecureHash"];
                string tmnCode = context.Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                string bankCode = context.Request.Query["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        await _transactionService.UpdateTransactionStatus(orderId,
                            (int)TransactionStatusEnum.Successful);
                        return 1;
                    }
                    else
                    {
                        await _transactionService.UpdateTransactionStatus(orderId,
                            (int)TransactionStatusEnum.Failed);
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}