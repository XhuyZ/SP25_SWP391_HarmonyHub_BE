using Domain.Constants;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class CloudinaryController : ApiBaseController
{
    private readonly ICloudinaryService _cloudinaryService;
    public CloudinaryController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost("cloudinary")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            var result = await _cloudinaryService.UploadFile(file);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
