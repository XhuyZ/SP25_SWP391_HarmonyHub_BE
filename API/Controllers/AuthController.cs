using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class AuthController : ApiBaseController
{
    private readonly IAccountService _accountService;

    public AuthController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var result = await _accountService.Login(request);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized,
                    MessageConstants.INVALID_ACCOUNT_CREDENTIALS));
            }

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, MessageConstants.FAILED));
        }
    }
}