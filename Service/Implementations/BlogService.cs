﻿using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public BlogService(IMapper mapper, IBlogRepository blogRepository, ICloudinaryService cloudinaryService)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _cloudinaryService = cloudinaryService;
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

        public async Task<BlogResponse> GetBlogById(int id)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(id);
                return _mapper.Map<BlogResponse>(blog);
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

        public async Task<bool> SetBlogStatus(int blogId, int status)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(blogId);
                if (status != (int)BlogStatusEnum.Inactive && status != (int)BlogStatusEnum.Active)
                    throw new ServiceException("Invalid status. Only 0 (Inactive) or 1 (Active) are allowed.");
                if (blog.Status == (int)status)
                    throw new ServiceException($"Blog is already {status}.");
                if (blog == null)
                    throw new ServiceException("Blog not found.");
                blog.Status = status;
                await _blogRepository.UpdateAsync(blog);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error updating blog status: {e.Message}", e);
            }
        }

        public async Task<bool> UpdateBlogDetails(int blogId, UpdateBlogRequest request)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(blogId);
                if (blog == null)
                    throw new ServiceException("Blog not found.");

                _mapper.Map(request, blog);
                blog.Status = (int)BlogStatusEnum.Pending;

                await _blogRepository.UpdateAsync(blog);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error updating blog : {e.Message}", e);
            }
        }

        public async Task<BlogResponse> UpdateBlogAvatar(int id, IFormFile img)
        {
            try
            {
                var blog = await _blogRepository.GetByIdAsync(id);
                if (blog == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);

                var avatarUrl = await _cloudinaryService.UploadFile(img);

                blog.ImageUrl = avatarUrl;
                await _blogRepository.UpdateAsync(blog);

                return _mapper.Map<BlogResponse>(blog);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
    }
}
