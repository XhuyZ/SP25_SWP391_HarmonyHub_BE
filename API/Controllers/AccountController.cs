using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

    [Authorize(Roles = "Member")]
    [HttpGet("profile/{memberId}")]
    public async Task<IActionResult> GetMemberProfile(int memberId)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingAccount = await _accountService.GetAccountByEmail(userEmail);
            
            if (existingAccount.Id != memberId)
                return Forbid();

            var result = await _accountService.GetMemberProfile(memberId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [Authorize(Roles = "Member")]
    [HttpPut("profile/{memberId}")]
    public async Task<IActionResult> UpdateMemberProfile(int memberId, UpdateProfileRequest request)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingAccount = await _accountService.GetAccountByEmail(userEmail);
            
            if (existingAccount.Id != memberId)
                return Forbid();

            var result = await _accountService.UpdateMemberProfile(memberId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [Authorize(Roles = "Therapist")]
    [HttpGet("profile/{therapistId}")]
    public async Task<IActionResult> GetTherapistProfile(int therapistId)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingAccount = await _accountService.GetAccountByEmail(userEmail);

            if (existingAccount.Id != therapistId)
                return Forbid();
            var result = await _accountService.GetTherapistProfile(therapistId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("profile/{therapistId}")]
    public async Task<IActionResult> UpdateTherapistProfile(int therapistId, UpdateTherapistProfileRequest request)
    {
        try
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var existingAccount = await _accountService.GetAccountByEmail(userEmail);
            if (existingAccount.Id != therapistId)
                return Forbid();
            var result = await _accountService.UpdateTherapistProfile(therapistId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}