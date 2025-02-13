using AutoMapper.Execution;
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

    [HttpGet("therapists")]
    public async Task<IActionResult> GetAllTherapists()
    {
        try
        {
            var result = await _accountService.GetAllTherapists();

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("therapists/{therapistId}")]
    public async Task<IActionResult> GetTherapistDetails(int therapistId)
    {
        try
        {
            var result = await _accountService.GetTherapistDetails(therapistId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }


    [HttpPost("register/member")]
    public async Task<IActionResult> RegisterMember(RegisterMemberRequest request)
    {
        try
        {
            await _accountService.RegisterMember(request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("register/therapist")]
    public async Task<IActionResult> RegisterTherapist(RegisterTherapistRequest request)
    {
        try
        {
            await _accountService.RegisterTherapist(request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("members/{memberId}")]
    public async Task<IActionResult> GetMemberProfile(int memberId)
    {
        try
        {
            var result = await _accountService.GetMemberProfile(memberId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("members/{memberId}")]
    public async Task<IActionResult> UpdateMemberProfile(int memberId, UpdateProfileRequest request)
    {
        try
        {
            var result = await _accountService.UpdateMemberProfile(memberId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("profile/{therapistId}")]
    public async Task<IActionResult> GetTherapistProfile(int therapistId)
    {
        try
        {
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
            var result = await _accountService.UpdateTherapistProfile(therapistId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}