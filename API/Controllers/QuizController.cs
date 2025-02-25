using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Service;
using Service.Implementations;
using Domain.Constants;
using Domain.DTOs.Common;
using Service.Exceptions;
using Service.Interfaces;
using Domain.DTOs.Requests;

namespace API.Controllers
{
    [Route("api/Quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _quizService.GetAllQuizzes();

                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedical([FromBody] CreateQuizRequest request)
        {
            try
            {
                var quiz = await _quizService.CreateQuizAsync(request);
                return Ok(new { statusCode = 200, message = "Successful", data = quiz });
            }
         catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

    }
}
