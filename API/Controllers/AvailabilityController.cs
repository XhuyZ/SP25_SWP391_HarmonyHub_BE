using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Implementations;
using Service.Interfaces;

namespace API.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpPost("availability/therapist/{id}")]
        public async Task<IActionResult> CreateAppointment(int id, UpdateAvailabilityRequest request)
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
    }
}
