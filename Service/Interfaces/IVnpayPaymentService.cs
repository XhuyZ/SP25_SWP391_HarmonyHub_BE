using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using VNPAY.NET.Models;

namespace Service.Interfaces
{
    public interface IVnpayPaymentService
    {
        string CreatePaymentUrl(double moneyToPay, string description, string ipAddress);
        PaymentResult GetPaymentCallback(IQueryCollection queryParams);
        PaymentResult ProcessIpnCallback(IQueryCollection queryParams);
        VnpayPaymentResponse ProcessPaymentReturn(IQueryCollection queryParams);
    }
}
