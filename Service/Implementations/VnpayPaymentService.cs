using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;

namespace Service.Implementations
{
    public class VnpayPaymentService : IVnpayPaymentService
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;

        public VnpayPaymentService(IVnpay vnpay, IConfiguration configuration)
        {
            _vnpay = vnpay;
            _configuration = configuration;

            _vnpay.Initialize(
                _configuration["Vnpay:TmnCode"],
                _configuration["Vnpay:HashSecret"],
                _configuration["Vnpay:BaseUrl"],
                _configuration["Vnpay:ReturnUrl"]
            );
        }

        public string CreatePaymentUrl(double moneyToPay, string description, string ipAddress)
        {
            var request = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = moneyToPay,
                Description = description,
                IpAddress = ipAddress,
                BankCode = BankCode.ANY,
                CreatedDate = DateTime.Now,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese
            };

            return _vnpay.GetPaymentUrl(request);
        }

        public PaymentResult GetPaymentCallback(IQueryCollection queryParams)
        {
            return _vnpay.GetPaymentResult(queryParams);
        }

        public PaymentResult ProcessIpnCallback(IQueryCollection queryParams)
        {
            return _vnpay.GetPaymentResult(queryParams);
        }
        public VnpayPaymentResponse ProcessPaymentReturn(IQueryCollection queryParams)
        {
            var paymentResult = _vnpay.GetPaymentResult(queryParams);

            return new VnpayPaymentResponse
            {
                PaymentId = queryParams["vnp_TxnRef"].ToString(),
                Amount = decimal.Parse(queryParams["vnp_Amount"].ToString()) / 100, // VNPAY trả về số tiền x100
                BankCode = queryParams["vnp_BankCode"].ToString(),
                BankTransactionNo = queryParams["vnp_BankTranNo"].ToString(),
                CardType = queryParams["vnp_CardType"].ToString(),
                OrderInfo = queryParams["vnp_OrderInfo"].ToString(),
                PaymentDate = DateTime.ParseExact(
                    queryParams["vnp_PayDate"].ToString(),
                    "yyyyMMddHHmmss",
                    CultureInfo.InvariantCulture
                ),
                TransactionNo = queryParams["vnp_TransactionNo"].ToString(),
                TransactionStatus = queryParams["vnp_TransactionStatus"].ToString(),
                PaymentStatus = paymentResult.IsSuccess,
                PaymentStatusMessage = paymentResult.IsSuccess
                    ? "Thanh toán thành công"
                    : "Thanh toán thất bại"
            };
        }
    }
}
