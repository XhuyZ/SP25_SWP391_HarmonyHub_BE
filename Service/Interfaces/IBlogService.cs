using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces;

public interface IBlogService
{
    Task CreateBlog(CreateBlogRequest request);
    Task<IEnumerable<BlogResponse>> GetAllBlogs();
    Task<IEnumerable<BlogResponse>> GetBlogsByTherapistId(int therapistId);
    Task<bool> SetBlogStatus(int blogId, int status);
    Task<BlogResponse> GetBlogById(int id);
    Task<bool> UpdateBlogDetails(int blogId, UpdateBlogRequest request);
    Task<BlogResponse> UpdateBlogAvatar(int id, IFormFile img);
}