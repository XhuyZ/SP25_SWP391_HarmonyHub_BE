using Domain.DTOs.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        //Task<IEnumerable<Blog>> GetBlogsByTherapistIdAsync(int therapistId);
        Task CreateBlog(CreateBlogRequest request);
    }
}
