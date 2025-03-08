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

    [HttpGet("member/profile/{memberId}")]
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

    [HttpPut("member/update-profile/{memberId}")]
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

    [HttpGet("therapist/profile/{therapistId}")]
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

    [HttpPut("therapist/update-profile/{therapistId}")]
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
    [HttpPut("member/update-detail/{memberId}")]
    public async Task<IActionResult> UpdateMemberInfo(int memberId, UpdateMemberInfoRequest request)
    {
        try
        {
            var result = await _accountService.UpdateMemberInfo(memberId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpPut("therapist/update-detail/{therapistId}")]
    public async Task<IActionResult> UpdateTherapistInfo(int therapistId, UpdateTherapistInfoRequest request)
    {
        try
        {
            var result = await _accountService.UpdateTherapistInfo(therapistId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpGet("therapist/qualification/{therapistid}")]
    public async Task<IActionResult> GetTherapistQualification(int therapistId)
    {
        try
        {
            var result = await _accountService.GetTherapistQualification(therapistId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpPut("therapist/update-qualification/{therapistid}")]
    public async Task<IActionResult> UpdateTherapistQualification(int therapistId, UpdateTherapistQualificationRequest request)
    {
        try
        {
            var result = await _accountService.UpdateTherapistQualification(therapistId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("accounts/{id}/avatar")]
    public async Task<IActionResult> UpdateAccountAvatar(int Id, IFormFile avatarFile)
    {
        try
        {
            var result = await _accountService.UpdateAvatarUrl(Id, avatarFile);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

}