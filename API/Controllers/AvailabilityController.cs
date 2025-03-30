using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class AvailabilityController : ApiBaseController
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpPut("availabilities/{id}")]
    public async Task<IActionResult> UpdateAvailability(int id, [FromBody] UpdateAvailabilityRequest request)
    {
        try
        {
            await _availabilityService.UpdateAvailability(id, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpDelete("availabilities/{id}")]
    public async Task<IActionResult> DeleteAvailability(int id)
    {
        try
        {
            await _availabilityService.DeleteAvailability(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpPost("availability")]
    public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityRequest request)
    {
        try
        {
            await _availabilityService.CreateAvailability(request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

}