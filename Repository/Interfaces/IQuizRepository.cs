using Domain.Entities;

namespace Repository.Interfaces;

public interface IQuizRepository : IGenericRepository<Quiz>
{
    Task<IEnumerable<Quiz>> GetAllQuizzes();
    Task<Quiz> GetByIdAsync(int id);
}