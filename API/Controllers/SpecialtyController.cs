using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;
[ApiController]
public class SpecialtyController : ApiBaseController
{
    private readonly ISpecialtyService _specialtyService;
    public SpecialtyController(ISpecialtyService specialtyService)
    {
        _specialtyService = specialtyService;
    }
    [HttpGet("specialties")]
    public async Task<IActionResult> GetAllSpecialties()
    {
        try
        {
            var result = await _specialtyService.GetAllSpecialties();
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpGet("specialties/{id}")]
    public async Task<IActionResult> GetSpecialtyByID(int id)
    {
        try
        {
            var result = await _specialtyService.GetSpecialtyByID(id);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("specialty")]
    public async Task<IActionResult> CreateSpecialty([FromBody] CreateSpecialtyRequest request)
    {
        try
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            await _specialtyService.CreateSpecialty(request);
            return Ok(new { message = "Specialty created successfully." });
        }
        catch (ServiceException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }
    }

}
