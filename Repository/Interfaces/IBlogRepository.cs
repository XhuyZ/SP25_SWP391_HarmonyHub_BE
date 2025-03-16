using System.Linq.Expressions;
using Domain.Entities;

namespace Repository.Interfaces;

public interface IBlogRepository : IGenericRepository<Blog>
{
    Task<IEnumerable<Blog>> GetAllAsync(Expression<Func<Blog, bool>> filter);

}