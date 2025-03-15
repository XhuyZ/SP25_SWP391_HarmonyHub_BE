using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
public class BlogController : ApiBaseController
{
    private readonly IBlogService _blogService;
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpPost("blogs")]
    public async Task<IActionResult> CreateBlog(CreateBlogRequest request)
    {
        try
        {
            await _blogService.CreateBlog(request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("blogs")]
    public async Task<IActionResult> GetAllBlogs()
    {
        try
        {
            var result = await _blogService.GetAllBlogs();

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("blogs/{id}")]
    public async Task<IActionResult> GetBlogById(int id)
    {
        try
        {
            var result = await _blogService.GetBlogById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, result));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpGet("therapists/{therapistId}/blogs")]
    public async Task<IActionResult> GetBlogsByTherapistId(int therapistId)
    {
        var blogs = await _blogService.GetBlogsByTherapistId(therapistId);

        if (!blogs.Any())
            return NotFound($"No blogs found for TherapistId {therapistId}");

        return Ok(new { statusCode = 200, message = "Successful", data = blogs });
    }

    [HttpPut("blogs/{id}/status")]
    public async Task<IActionResult> SetBlogStatus(int id, int status)
    {
        var success = await _blogService.SetBlogStatus(id, status);
        if (!success)
            return BadRequest(new { message = "Failed to update blog status." });

        return Ok(new { statusCode = 200, message = "Blog status updated." });
    }

    [HttpPut("blogs/{id}")]
    public async Task<IActionResult> UpdateBlogDetails(int id, UpdateBlogRequest request)
    {
        try
        {
            await _blogService.UpdateBlogDetails(id, request);
            return Ok(new ApiResponse(StatusCodes.Status200OK, MessageConstants.SUCCESSFUL, request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}
