using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVnpayPaymentService _vnpayService;
        private readonly IConfiguration _configuration;

        public VNPayController(IVnpayPaymentService vnpayService)
        {
            _vnpayService = vnpayService;
        }
        [HttpGet("CreatePaymentUrl")]
        public ActionResult<string> CreatePaymentUrl(double moneyToPay, string description)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1";
                var paymentUrl = _vnpayService.CreatePaymentUrl(moneyToPay, description, ipAddress);
                return Created(paymentUrl, paymentUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Callback")]
        public ActionResult<string> Callback()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpayService.GetPaymentCallback(Request.Query);
                    var resultDescription = $"{paymentResult.PaymentResponse.Description}. {paymentResult.TransactionStatus.Description}.";

                    if (paymentResult.IsSuccess)
                    {
                        return Ok(resultDescription);
                    }

                    return BadRequest(resultDescription);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }

        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpayService.ProcessIpnCallback(Request.Query);
                    if (paymentResult.IsSuccess)
                    {
                        // Thực hiện cập nhật trạng thái đơn hàng trong DB của bạn tại đây
                        return Ok();
                    }

                    return BadRequest("Thanh toán thất bại");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }

        [HttpGet("vnpay-return")]
        public IActionResult PaymentReturn()
        {
            try
            {
                var response = _vnpayService.ProcessPaymentReturn(Request.Query);

                if (response.PaymentStatus)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "Thanh toán thành công",
                        Data = response
                    });
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Thanh toán thất bại",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
