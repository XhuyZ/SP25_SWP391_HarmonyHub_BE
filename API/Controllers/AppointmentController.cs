using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class AppointmentController : ApiBaseController
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("members/{memberId}/appointments")]
    public async Task<IActionResult> GetMemberAppointments(int memberId)
    {
        try
        {
            var result = await _appointmentService.GetMemberAppointments(memberId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpGet("therapists/{therapistId}/appointments")]
    public async Task<IActionResult> GetTherapistAppointments(int therapistId)
    {
        try
        {
            var result = await _appointmentService.GetTherapistAppointments(therapistId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpGet("appointments/{appointmentId}")]
    public async Task<IActionResult> GetAppointmentById(int appointmentId)
    {
        try
        {
            var result = await _appointmentService.GetAppointmentById(appointmentId);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpPost("members/{memberId}/appointments")]
    public async Task<IActionResult> CreateAppointment(int memberId, CreateAppointmentRequest request)
    {
        try
        {
            await _appointmentService.CreateAppointment(memberId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpPut("appointments/{appointmentId}/status")]
    public async Task<IActionResult> ChangeAppointmentStatus(int appointmentId, ChangeAppointmentStatusRequest request)
    {
        try
        {
            await _appointmentService.ChangeAppointmentStatus(appointmentId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
    [HttpGet("appointments/feedback")]
    public async Task<IActionResult> GetAllAppointmentFeedback()
    {
        try
        {
            var result = await _appointmentService.GetAllAppointmentFeedback();
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    //[HttpGet("appointments/feedback/{appointmentId}")]
    //public async Task<IActionResult> GetAppointmentFeedbackID(int appointmentId)
    //{
    //    try
    //    {
    //        var result = await _appointmentService.GetAppointmentFeedbackID(appointmentId);
    //        return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
    //    }
    //    catch (Exception e)
    //    {
    //        throw new ServiceException(e.Message);
    //    }
    //}

    //[HttpPost("appointments/{appointmentId}/create-feedback")]
    //public async Task<IActionResult> CreateFeedbackAppointment(int appointmentId, CreateFeedbackAppointmentRequest request)
    //{
    //    try
    //    {
    //        await _appointmentService.CreateFeedbackAppointment(appointmentId, request);
    //        return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
    //    }
    //    catch (Exception e)
    //    {
    //        throw new ServiceException(e.Message);
    //    }
    //}
    [HttpPut("appointments/update-feedback/{appointmentId}")]
    public async Task<IActionResult> UpdateFeedbackAppointment(int appointmentId, UpdateFeedbackAppointmentRequest request)
    {
        try
        {
            await _appointmentService.UpdateFeedbackAppointment(appointmentId, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    [HttpPut("appintments/delete-feedback/{appointmentId}")]
    public async Task<IActionResult> DeleteFeedbackAppointment(int appointmentId) 
    {
        try
        {
            await _appointmentService.DeleteFeedbackAppointment(appointmentId);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("therapists/appointments/update-appointment-note/{appointmentid}")]
    public async Task<IActionResult> UpdateTherapistAppointmentNote(int appointmentId, UpdateTherapistAppointmentRequest request)
    {
        try
        {
            await _appointmentService.UpdateAppointmentNote(appointmentId, request);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }
}