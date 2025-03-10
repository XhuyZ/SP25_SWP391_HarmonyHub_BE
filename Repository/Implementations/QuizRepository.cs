using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class QuizRepository : GenericRepository<Quiz>, IQuizRepository
    {
        public async Task<IEnumerable<Quiz>> GetAllQuizzes()
        {
            return await _context.Quizzes
                .AsNoTracking()
                .AsSplitQuery()
                    .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Question)
                    .ThenInclude(q => q.Options)
                     .Include(q => q.Results)
                .ToListAsync();
        }

        public async Task<Quiz> GetByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Question)
                        .ThenInclude(q => q.Options)
                         .Include(q => q.Results)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

    }
}
