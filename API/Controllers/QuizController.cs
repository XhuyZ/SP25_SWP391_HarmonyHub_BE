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

        // GET: api/quiz
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

        //// GET: api/quiz/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Quiz>> GetById(int id)
        //{
        //    var quiz = await _quizService.GetQuizByIdAsync(id);
        //    if (quiz == null)
        //        return NotFound(new { message = "Quiz not found" });

        //    return Ok(quiz);
        //}

        //// POST: api/quiz
        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody] Quiz quiz)
        //{
        //    if (quiz == null)
        //        return BadRequest(new { message = "Invalid quiz data" });

        //    await _quizService.AddQuizAsync(quiz);
        //    return CreatedAtAction(nameof(GetById), new { id = quiz.Id }, quiz);
        //}

        //// PUT: api/quiz/{id}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] Quiz quiz)
        //{
        //    if (quiz == null || quiz.Id != id)
        //        return BadRequest(new { message = "Quiz data is invalid or mismatched ID" });

        //    var existingQuiz = await _quizService.GetQuizByIdAsync(id);
        //    if (existingQuiz == null)
        //        return NotFound(new { message = "Quiz not found" });

        //    await _quizService.UpdateQuizAsync(quiz);
        //    return NoContent();
        //}
    }
}
