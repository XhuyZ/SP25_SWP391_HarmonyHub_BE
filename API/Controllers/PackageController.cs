using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class PackageController : ApiBaseController
{
    private readonly IPackageService _packageService;

    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpPost("therapists/{therapistId}/packages")]
    public async Task<IActionResult> CreatePackage(int therapistId, CreatePackageRequest request)
    {
        try
        {
            await _packageService.CreatePackage(therapistId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("packages/{packageId}/status")]
    public async Task<IActionResult> ChangePackageStatus(int packageId, ChangePackageStatusRequest request)
    {
        try
        {
            await _packageService.ChangePackageStatus(packageId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}