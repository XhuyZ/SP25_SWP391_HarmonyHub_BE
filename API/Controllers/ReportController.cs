using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;

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
    public async Task<IActionResult> GetReportsByAcountID(int reportID)
    {
        try
        {
            var result = await _reportService.GetReportsByID(reportID);
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
    [HttpDelete("reports/{reportID}")]
    public async Task<IActionResult> DeleteReport(int reportID)
    {
        try
        {
            var result = await _reportService.DeleteReport(reportID);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
