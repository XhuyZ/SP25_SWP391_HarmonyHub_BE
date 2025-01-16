using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers
{
    public class FeedbackController : ApiBaseController
    {
        public readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var result = await _feedbackService.GetAllFeedbacks();
                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpGet("{accountID}")]
        public async Task<IActionResult> GetFeedbackByMemberId(int memberId)
        {
            try
            {
                var result = await _feedbackService.GetFeedbackByMemberId(memberId);
                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(CreateFeedbackRequest request)
        {
            try
            {
                await _feedbackService.CreateFeedback(request);
                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }
    }
}
