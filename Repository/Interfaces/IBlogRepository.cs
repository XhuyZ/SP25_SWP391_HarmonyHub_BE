using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Interfaces;

public interface IBlogRepository : IGenericRepository<Blog>
{
    Task<IEnumerable<Blog>> GetAllAsync(Expression<Func<Blog, bool>> filter);

}