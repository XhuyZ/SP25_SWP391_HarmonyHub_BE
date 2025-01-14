using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers;

[Route("cloudinary")]
[ApiController]

public class CloudinaryController : ApiBaseController
{
    private readonly ICloudinaryService _cloudinaryService;
    public CloudinaryController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        try
        {
            var result = await _cloudinaryService.UploadFile(file);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL,result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
