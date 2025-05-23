﻿using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    public class QuizController : ApiBaseController
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet("quiz")]
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

        [HttpGet("quiz/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _quizService.GetQuizById(id);

                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPost("quiz/create")]
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

        [HttpPut("quiz/SetStatus/{id}")]
        public async Task<IActionResult> SetStatusQuiz(int id, int status)
        {
            try
            {
                var result = await _quizService.SetQuizStatus(id, status);
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

        [HttpPut("quiz/imgUrl/{id}")]
        public async Task<IActionResult> UpdateQuizImgUrl(int id, IFormFile imgUrl)
        {
            try
            {
                var result = await _quizService.UpdateAvatarUrl(id, imgUrl);
                return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
            }
            catch (ServiceException e)
            {
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
            }
        }

        [HttpPut("quiz/{id}/results")]
        public async Task<IActionResult> UpdateQuizResults(int id, [FromBody] List<UpdateQuizResultRequest> request)
        {
            if (request == null || !request.Any())
                return BadRequest("Request cannot be empty.");

            var response = await _quizService.UpdateQuizResultsAsync(id, request);
            return Ok(new { statusCode = 200, message = "Results updated successfully", data = response });
        }

        [HttpDelete("questions/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var result = await _quizService.DeleteQuestionAsync(id);
                if (result)
                {
                    return Ok(new { Message = "Question and options deleted successfully." });
                }

                return NotFound($"Question with ID {id} not found.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { message = "An error occurred while deleting the question.", error = ex.Message });
            }
        }

        [HttpPut("quiz/{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, [FromBody] UpdateQuizRequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be null.");

            try
            {
                var updatedQuiz = await _quizService.UpdateQuizAsync(id, request);
                return Ok(updatedQuiz);
            }
            catch (ServiceException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }
    }
}