using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class AccountController : ApiBaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAllAccounts()
    {
        try
        {
            var result = await _accountService.GetAllAccounts();

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("accounts/{accountId}")]
    public async Task<IActionResult> GetAccountById(int accountId)
    {
        try
        {
            var result = await _accountService.GetAccountById(accountId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }


    [HttpPost("accounts")]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        try
        {
            await _accountService.CreateAccount(request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}