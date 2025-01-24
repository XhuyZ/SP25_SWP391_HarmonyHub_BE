using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        Task CreateBlog(CreateBlogRequest request);
        Task<IEnumerable<BlogResponse>> GetAllBlogs();

    }
}
