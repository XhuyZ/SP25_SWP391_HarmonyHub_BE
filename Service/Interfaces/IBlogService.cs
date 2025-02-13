using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        Task CreateBlog(CreateBlogRequest request);
        Task<IEnumerable<BlogResponse>> GetAllBlogs();
        Task<IEnumerable<BlogResponse>> GetBlogsByTherapistId(int therapistId);
        Task<bool> SetBlogInactive(int blogId);
        Task<bool> SetBlogActive(int blogId);

    }
}
