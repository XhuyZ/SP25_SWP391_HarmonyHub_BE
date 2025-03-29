using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class ReportController : ApiBaseController
{
    public readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("reports")]
    public async Task<IActionResult> GetAllReports()
    {
        try
        {
            var result = await _reportService.GetAllReports();
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("reports/{reportID}")]
    public async Task<IActionResult> GetReportsByReportId(int reportID)
    {
        try
        {
            var result = await _reportService.GetReportByID(reportID);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost("reports")]
    public async Task<IActionResult> CreateReport(CreateReportRequest request)
    {
        try
        {
            await _reportService.CreateReport(request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("reports/{reportID}")]
    public async Task<IActionResult> UpdateReport(int reportID, UpdateReportRequest request)
    {
        try
        {
            var result = await _reportService.UpdateReport(reportID, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("reports/{id}/status")]
    public async Task<IActionResult> SetReportStatus(int id, int status)
    {
        var success = await _reportService.UpdateStatus(id, status);
        if (!success)
            return BadRequest(new { message = "Failed to update report status." });

        return Ok(new { statusCode = 200, message = "report status updated." });
    }
}