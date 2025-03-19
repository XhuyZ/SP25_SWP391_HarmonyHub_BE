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

    [HttpGet("members/{memberId}")]
    public async Task<IActionResult> GetMemberDetails(int memberId)
    {
        try
        {
            var result = await _accountService.GetMemberDetails(memberId);
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

    [HttpPut("members/{memberId}/profile")]
    public async Task<IActionResult> UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request)
    {
        try
        {
            await _accountService.UpdateMemberInfo(memberId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("therapists/{therapistId}/profile")]
    public async Task<IActionResult> UpdateTherapistInfo(int therapistId, UpdateTherapistInfoRequest request)
    {
        try
        {
            await _accountService.UpdateTherapistInfo(therapistId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("qualifications")]
    public async Task<IActionResult> AddTherapistQualification(AddQualificationRequest request)
    {
        try
        {
            await _accountService.AddTherapistQualification(request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("qualifications/{qualificationId}")]
    public async Task<IActionResult> UpdateTherapistQualification(int therapistId, int qualificationId,
        UpdateTherapistQualificationRequest request)
    {
        try
        {
            await _accountService.UpdateTherapistQualification(qualificationId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("accounts/{id}/avatar")]
    public async Task<IActionResult> UpdateAccountAvatar(int id, IFormFile avatarFile)
    {
        try
        {
            var result = await _accountService.UpdateAvatarUrl(id, avatarFile);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("accounts/{id}/status")]
    public async Task<IActionResult> UpdateAccountStatus(int id, int status)
    {
        try
        {
            var result = await _accountService.UpdateAccountStatus(id, status);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}