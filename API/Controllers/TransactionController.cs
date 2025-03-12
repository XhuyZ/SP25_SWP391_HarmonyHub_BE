using Domain.Constants;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class TransactionController : ApiBaseController
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetAllTransactions()
    {
        try
        {
            var result = await _transactionService.GetAllTransactions();
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("transactions/{transactionId}")]
    public async Task<IActionResult> GetTransactionByTransactionId(string transactionId)
    {
        try
        {
            var result = await _transactionService.GetTransactionByTransactionId(transactionId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}