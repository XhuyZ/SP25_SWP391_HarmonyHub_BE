using Domain.Constants;
using Domain.DTOs.Common;
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
}
