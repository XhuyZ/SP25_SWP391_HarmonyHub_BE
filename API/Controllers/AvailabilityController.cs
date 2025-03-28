using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Exceptions;
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

        [HttpPost("availabilities/{id}")]
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] UpdateAvailabilityRequest request)
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

        [HttpDelete("availabilities/{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            try
            {
                await _availabilityService.DeleteAvailability(id);

                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

    }
}
