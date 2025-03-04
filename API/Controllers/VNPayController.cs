using Domain.Constants;
using Domain.DTO.Request;
using System.Net;
using Domain.DTOs.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Service.Interfaces;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using Domain.DTOs.Responses;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVnpayPaymentService _vnpayService;
        private readonly IGenericRepository<Account> _accountRepo;
        private readonly IGenericService<Transaction, TransactionDTO> _transactionService;

        public VNPayController(IHttpContextAccessor httpContextAccessor, IVnpayPaymentService vnpayService, IGenericRepository<Account> accountRepo
            , IGenericService<Transaction, TransactionDTO> transactionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _vnpayService = vnpayService;
            _accountRepo = accountRepo;
            _transactionService = transactionService;
        }
        [HttpPost("Pay")]
        public async Task<ActionResult<ApiResponse>> CreatePayment([FromBody] CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                string payload = _vnpayService.CreatePayment(createPaymentRequest);

                return Ok(new ApiResponse((int)HttpStatusCode.OK, MessageConstants.SUCCESSFUL, payload));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, MessageConstants.FAILED, null));
            }
        }

        [HttpPost("CheckUserCredit")]
        public async Task<ActionResult<ApiResponse>> CheckPaymentForUserCredit([FromBody] CreatePaymentUserRequest createPaymentRequest)
        {
            try
            {
                var user = await _accountRepo.GetByIdAsync(createPaymentRequest.UserId);
                if (user == null)
                {
                    return NotFound($"User with ID {createPaymentRequest.UserId} not found.");
                }
                if (user.Balance < createPaymentRequest.Amount)
                {
                    return BadRequest(createPaymentRequest.Amount - user.Balance);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, MessageConstants.FAILED, null));
            }
        }

        [HttpPost("PayUserCredit")]
        public async Task<ActionResult<ApiResponse>> CreatePaymentForUserCredit([FromBody] CreatePaymentUserRequest createPaymentRequest)
        {
            try
            {
                string payload = _vnpayService.CreatePaymentForUserCredit(createPaymentRequest);

                return Ok(new ApiResponse((int)HttpStatusCode.OK, MessageConstants.SUCCESSFUL, payload));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, MessageConstants.FAILED, null));
            }
        }

        [HttpGet("Result/{Id}")]
        public async Task<ActionResult<ApiResponse>> GetPaymentResultUser(int Id)
        {
            var context = _httpContextAccessor.HttpContext;

            try
            {
                int result = _vnpayService.GetPaymentResult();

                string orderId = context.Request.Query["vnp_TxnRef"];
                string amount = context.Request.Query["vnp_Amount"];
                string orderInfo = context.Request.Query["vnp_OrderInfo"];
                string payDate = context.Request.Query["vnp_PayDate"];
                string transactionCode = context.Request.Query["vnp_TransactionNo"];

                var user = await _accountRepo.GetByIdAsync(Id);
                //var role = user.Role;
                if (user != null && result == 1)
                {
                    var transaction = new TransactionDTO
                    {
                        UserId = Id,
                        ReceiverId = Id,
                        Amount = decimal.Parse(amount) / 100,
                        TransactionId = transactionCode,
                        Description = "Charge credit for user",
                        PaymentMethod  = (int)TransactionConstant.CHARGE,
                        Status = (int)TransactionStatusEnum.Created,
                    };

                    await _transactionService.Add(transaction);

                    user.Balance = user.Balance + decimal.Parse(amount) / 100;
                    await _accountRepo.UpdateAsync(user);
                }
                //if (role == "Therapist")
                //{
                //    return Redirect("http://localhost:3000/ProfileTutor");
                //}
                return Redirect("http://localhost:3000/Profile");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, MessageConstants.FAILED, null));
            }

        }

        [HttpGet("Result")]
        public async Task<ActionResult<ApiResponse>> GetPaymentResult()
        {
            var context = _httpContextAccessor.HttpContext;

            try
            {
                int result = _vnpayService.GetPaymentResult();

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

                return Ok(new ApiResponse((int)HttpStatusCode.OK, MessageConstants.SUCCESSFUL, createPaymentResponse));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, MessageConstants.FAILED, null));
            }

        }
    }
}
