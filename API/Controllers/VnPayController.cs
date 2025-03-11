using Domain.Constants;
using Domain.DTO.Request;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Exceptions;
using Service.Interfaces;
using Service.Settings;

namespace API.Controllers
{
    [ApiController]
    public class VnPayController : ApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVnPayService _vnPayService;
        private readonly VnPaySettings _vnPaySettings;

        public VnPayController(IHttpContextAccessor httpContextAccessor, IVnPayService vnPayService, IOptions<VnPaySettings> vnPaySettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnPayService = vnPayService;
            _vnPaySettings = vnPaySettings.Value;
        }

        [HttpPost("vnpay/pay")]
        public async Task<ActionResult<ApiResponse>> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                string payload = await _vnPayService.CreatePayment(createPaymentRequest);

                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, payload));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, MessageConstants.FAILED, null));
            }
        }

        [HttpGet("vnpay/result")]
        public async Task<IActionResult> GetPaymentResult()
        {
            var context = _httpContextAccessor.HttpContext;

            try
            {
                int result = await _vnPayService.GetPaymentResult();

                string orderId = context.Request.Query["vnp_TxnRef"];
                string amount = context.Request.Query["vnp_Amount"];
                string orderInfo = context.Request.Query["vnp_OrderInfo"];
                string payDate = context.Request.Query["vnp_PayDate"];

                return Redirect($"{_vnPaySettings.RedirectUrl}?result={result}&orderId={orderId}&amount={amount}&orderInfo={orderInfo}&payDate={payDate}");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, MessageConstants.FAILED, null));
            }
        }
    }
}