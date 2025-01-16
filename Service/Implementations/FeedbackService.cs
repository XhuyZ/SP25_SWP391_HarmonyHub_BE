using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

    public class FeedbackService: IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IMapper mapper, IFeedbackRepository feedbackRepository)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<FeedbackResponse>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<FeedbackResponse>>(feedbacks);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task<FeedbackResponse> GetFeedbackByMemberId(int memberId)
        {
            try
            {
                var feedback = await _feedbackRepository.GetByIdAsync(memberId);
                if (feedback == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);
                return _mapper.Map<FeedbackResponse>(feedback);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        public async Task CreateFeedback(CreateFeedbackRequest request)
        {
            try
            {
                var feedback = _mapper.Map<Feedback>(request);
                await _feedbackRepository.AddAsync(feedback);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        //public async Task DeleteFeedback
    }


