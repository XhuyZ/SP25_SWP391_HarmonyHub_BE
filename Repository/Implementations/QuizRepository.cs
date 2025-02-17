using Domain.Entities;
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
    }
}
