using Domain.Entities;
using Repository.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Constants;


namespace Repository.Implementations;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public async Task<IEnumerable<Blog>> GetAllAsync(Expression<Func<Blog, bool>> filter)
    {
        return await _context.Blogs.Where(filter).ToListAsync();
    }

}