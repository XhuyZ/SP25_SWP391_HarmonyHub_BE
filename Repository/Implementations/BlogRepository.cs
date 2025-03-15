using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implementations;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public async Task<IEnumerable<Blog>> GetAllAsync(Expression<Func<Blog, bool>> filter)
    {
        return await _context.Blogs.Where(filter).ToListAsync();
    }

}