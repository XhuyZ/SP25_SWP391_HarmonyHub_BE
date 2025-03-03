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
using MySqlX.XDevAPI.Common;

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

        [HttpGet("GetAllQuizzes")]
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

        [HttpPost("CreateQuizWithQuestion(s)AndOption(s)")]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest request)
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

        [HttpPut("{id}/SetStatus")]
        public async Task<IActionResult> SetStatusQuiz(int id, int status)
        {
            try
            {
                var result = await _quizService.SetQuizStatus(id,status);
                return Ok(new { Message = "Quiz status updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("question")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var result = await _quizService.DeleteQuestionAsync(questionId);
                if (result)
                {
                    return Ok(new { Message = "Question and options deleted successfully." });
                }
                return NotFound($"Question with ID {questionId} not found.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the question.", error = ex.Message });
            }
        }
    }
}
