using AutoMapper;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        // Get all quizzes
        public async Task<IEnumerable<QuizResponse>> GetAllQuizzes()
        {
            try
            {
                var quiz = await _quizRepository.GetAllQuizzes();
                return _mapper.Map<IEnumerable<QuizResponse>>(quiz);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        //// Get a quiz by ID
        //public async Task<Quiz> GetQuizByIdAsync(int id)
        //{
        //    return await _quizRepository.GetByIdAsync(id);
        //}

        //// Add a new quiz
        public async Task<IEnumerable<QuizResponse>> AddQuizAsync(CreateQuizRequest request)
        {

            return await _quizRepository.AddAsync(request);
        }

        //// Update an existing quiz
        //public async Task UpdateQuizAsync(Quiz quiz)
        //{
        //    await _quizRepository.UpdateAsync(quiz);
        //}

        //// Delete a quiz by ID
        //public async Task DeleteQuizAsync(Quiz quiz)
        //{
        //    await _quizRepository.DeleteAsync(quiz);
        //}
    }
}
