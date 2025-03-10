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
    [HttpGet("reports/{accountId}")]
    public async Task<IActionResult> GetReportsByAcountID(int accountId)
    {
        try
        {
            var result = await _reportService.GetReportsByAcountID(accountId);
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
    [HttpPut("reports/{accountId}")]
    public async Task<IActionResult> UpdateReport(int accountId, UpdateReportRequest request)
    {
        try
        {
            var result = await _reportService.UpdateReport(accountId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpDelete("reports/{accountId}")]
    public async Task<IActionResult> DeleteReport(int accountId)
    {
        try
        {
            var result = await _reportService.DeleteReport(accountId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
