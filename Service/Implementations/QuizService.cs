using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IQuestionRepository _questionRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public QuizService(IQuizRepository quizRepository, IMapper mapper, IQuestionRepository questionRepository, IOptionRepository optionRepository, IQuizQuestionRepository quizQuestionRepository, ICloudinaryService cloudinaryService)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _optionRepository = optionRepository;
            _quizQuestionRepository = quizQuestionRepository;
            _cloudinaryService = cloudinaryService;
        }


        public async Task<bool> SetQuizStatus(int quizId, int status)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(quizId);
                if (status != (int)QuizStatusEnum.Inactive && status != (int)QuizStatusEnum.Active)
                    throw new ServiceException("Invalid status. Only 0 (Inactive) or 1 (Active) are allowed.");
                if (quiz.Status == (int)status)
                    throw new ServiceException($"Quiz is already {status}.");
                if (quiz == null)
                    throw new ServiceException("Quiz not found.");
                quiz.Status = status;
                await _quizRepository.UpdateAsync(quiz);
                return true;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Error updating quiz status: {e.Message}", e);
            }
        }

        public async Task<IEnumerable<QuizResponse>> GetAllQuizzes()
        {
            try
            {
                var quizzes = await _quizRepository.GetAllQuizzes();
                return _mapper.Map<IEnumerable<QuizResponse>>(quizzes);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task<QuizResponse> GetQuizById(int id)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                return _mapper.Map<QuizResponse>(quiz);
            }catch(Exception e)
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
                Status = (int)QuizStatusEnum.Pending,
                TherapistId = request.TherapistId,
                QuizQuestions = new List<QuizQuestion>(),
                Results = new List<Result>()
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

                foreach (var resultRequest in request.Results)
                {
                    var result = new Result
                    {
                        Content = resultRequest.Content,
                        Quiz = quiz
                    };
                    quiz.Results.Add(result);
                }

            await _quizRepository.AddAsync(quiz);
            return _mapper.Map<QuizResponse>(quiz);
        }


        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (question == null)
            {
                throw new KeyNotFoundException($"Question with ID {questionId} not found.");
            }

            var quizQuestions = (await _quizQuestionRepository.GetAllAsync()).Where(q => q.QuestionId == questionId);
            if (quizQuestions.Any())
            {
                await _quizQuestionRepository.DeleteRangeAsync(quizQuestions);
            }

            var options = (await _optionRepository.GetAllAsync()).Where(o => o.QuestionId == questionId);
            if (options.Any())
            {
                await _optionRepository.DeleteRangeAsync(options);
            }

            await _questionRepository.DeleteAsync(question);

            return true;
        }

        public async Task<QuizResponse> UpdateAvatarUrl(int id, IFormFile imgFile)
        {
            try
            {
                var quiz = await _quizRepository.GetByIdAsync(id);
                if (quiz == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);

                var imgUrl = await _cloudinaryService.UploadFile(imgFile);

                quiz.ImageUrl = imgUrl;
                await _quizRepository.UpdateAsync(quiz);

                return _mapper.Map<QuizResponse>(quiz);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task<List<ResultResponse>> UpdateQuizResultsAsync(int id, List<UpdateQuizResultRequest> request)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
                throw new ServiceException("Quiz not found.");

            foreach (var resultRequest in request)
            {
                if (resultRequest.Id <= 0)
                    throw new ServiceException("Result ID is required and must be greater than zero.");

                var existingResult = quiz.Results.FirstOrDefault(r => r.Id == resultRequest.Id);
                if (existingResult == null)
                    throw new ServiceException($"No result with ID {resultRequest.Id} is linked to the quiz with ID {id}.");

                existingResult.Content = resultRequest.Content;
            }

            await _quizRepository.UpdateAsync(quiz);

            return quiz.Results.Select(r => new ResultResponse
            {
                Id = r.Id,
                Content = r.Content
            }).ToList();
        }

    }
}
