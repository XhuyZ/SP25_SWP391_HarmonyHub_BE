using Domain.Constants;
using Domain.DTO.Request;
using Domain.DTOs.Common;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public class VnPayController : ApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVnPayService _vnPayService;

        public VnPayController(IHttpContextAccessor httpContextAccessor, IVnPayService vnPayService)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnPayService = vnPayService;
        }

        [HttpPost("vnpay/pay")]
        public async Task<ActionResult<ApiResponse>> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                string payload = _vnPayService.CreatePayment(createPaymentRequest);

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
                int result = _vnPayService.GetPaymentResult();

                string orderId = context.Request.Query["vnp_TxnRef"];
                string amount = context.Request.Query["vnp_Amount"];
                string orderInfo = context.Request.Query["vnp_OrderInfo"];
                string payDate = context.Request.Query["vnp_PayDate"];
                string userId = context.Request.Query["vnp_Billing_Email"];

                CreatePaymentResponse createPaymentResponse = new CreatePaymentResponse
                {
                    OrderId = orderId,
                    Amount = amount,
                    OrderInfo = orderInfo,
                    PayDate = payDate
                };

                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, createPaymentResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, MessageConstants.FAILED, null));
            }
        }
    }
}