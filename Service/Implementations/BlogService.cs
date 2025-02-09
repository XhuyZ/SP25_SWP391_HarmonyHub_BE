using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public BlogService(IMapper mapper, IBlogRepository blogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<BlogResponse>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<BlogResponse>>(blogs);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task CreateBlog(CreateBlogRequest request)
        {
            try
            {
                var blog = _mapper.Map<Blog>(request);
                blog.Status = (int)BlogStatusEnum.Pending;
                
                await _blogRepository.AddAsync(blog);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }


        public async Task UpdateBlogAsync(Blog blog)
        {
            await _blogRepository.UpdateAsync(blog);
        }

        public async Task<IEnumerable<BlogResponse>> GetBlogsByTherapistId(int therapistId)
        {
            try
            {
                var blogs = await _blogRepository.GetAllAsync(b => b.TherapistId.HasValue && b.TherapistId.Value == therapistId);
                return _mapper.Map<IEnumerable<BlogResponse>>(blogs);
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error retrieving blogs for TherapistId {therapistId}: {e.Message}", e);
            }
        }

        public async Task<bool> SetBlogInactive(int blogId)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(blogId);
                if (blog == null)
                    throw new ServiceException("Blog not found.");

                blog.Status = 0;
                await _blogRepository.UpdateAsync(blog);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error setting blog inactive: {e.Message}", e);
            }
        }

        public async Task<bool> SetBlogActive(int blogId)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(blogId);
                if (blog == null)
                    throw new ServiceException("Blog not found.");

                blog.Status = 1;
                await _blogRepository.UpdateAsync(blog);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error setting blog active: {e.Message}", e);
            }
        }

    }
}
