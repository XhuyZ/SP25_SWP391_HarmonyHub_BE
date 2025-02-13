using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Implementations;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly IAccountRepository _accountRepository;

        public ReportService(IMapper mapper, IReportRepository reportRepository, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<ReportResponse>> GetAllReports()
        {
            try
            {
                var report = await _reportRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ReportResponse>>(report);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public async Task<ReportResponse> GetReportsByAcountID(int acountID)
        {
            try
            {
                var report = await _reportRepository.GetByIdAsync(acountID);
                if (report == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);
                return _mapper.Map<ReportResponse>(report);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public async Task CreateReport(CreateReportRequest request)
        {
            try
            {
                var report = _mapper.Map<Report>(request);
                await _reportRepository.AddAsync(report);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public async Task<ReportResponse> UpdateReport(int acountID, UpdateReportRequest request)
        {
            try
            {
                var account = await _accountRepository.GetByIdAsync(acountID);

                if (account == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);
                if (account.Role != (int)RoleEnum.Therapist)
                    throw new ServiceException(MessageConstants.INVALID_ACCOUNT_CREDENTIALS);

                var report = await _reportRepository.GetByIdAsync(request.Id);
                if (report == null)
                    throw new ServiceException(MessageConstants.NOT_FOUND);
                report.Id = request.Id;
                report.Title = request.Title;
                report.Content = request.Content;
                report.AccountId = request.AccountId;
                report.CreatedAt = request.CreatedAt;
                report.UpdatedAt = DateTime.Now;

                await _reportRepository.UpdateAsync(report);
                return _mapper.Map<ReportResponse>(report);
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }
        public async Task<Report> DeleteReport(int acountID)
        {
            var report = await _reportRepository.GetAllAsync();
            var reportToDelete = report.FirstOrDefault(f =>
            f.AccountId == acountID);

            if (reportToDelete == null)
            {
                throw new ServiceException(MessageConstants.NOT_FOUND);
            }
            await _reportRepository.DeleteAsync(reportToDelete);
            return reportToDelete;
        }
    }
}
    

