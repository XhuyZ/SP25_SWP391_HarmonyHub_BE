using AutoMapper;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
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

        public async Task<QuizResponse> CreateQuizAsync(CreateQuizRequest request)
        {
            var quiz = new Quiz
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Status = request.Status,
                TherapistId = request.TherapistId,
                QuizQuestions = new List<QuizQuestion>()
            };

            foreach (var questionRequest in request.Questions)
            {
                var question = new Question
                {
                    Content = questionRequest.Content,
                    Options = new List<Option>()
                };

                foreach (var optionRequest in questionRequest.Options)
                {
                    var option = new Option
                    {
                        Content = optionRequest.Content
                    };
                    question.Options.Add(option);
                }

                var quizQuestion = new QuizQuestion
                {
                    Quiz = quiz,
                    Question = question
                };

                quiz.QuizQuestions.Add(quizQuestion);
            }

            await _quizRepository.AddAsync(quiz);

            return _mapper.Map<QuizResponse>(quiz);
        }

    }
}
