using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;
using Repository.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class ReportService : IReportService
{
    private readonly IMapper _mapper;
    private readonly IReportRepository _reportRepository;

    public ReportService(IMapper mapper, IReportRepository reportRepository)
    {
        _mapper = mapper;
        _reportRepository = reportRepository;
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

    public async Task<ReportResponse> GetReportByID(int reportID)
    {
        try
        {
            var report = await _reportRepository.GetByIdAsync(reportID);
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
            report.Status = (int)ReportStatusEnum.Pending;

            await _reportRepository.AddAsync(report);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<ReportResponse> UpdateReport(int reportID, UpdateReportRequest request)
    {
        var report = await _reportRepository.GetByIdAsync(reportID);
        if (report == null)
            throw new ServiceException(MessageConstants.NOT_FOUND);
        try
        {
            report.Title = request.Title;
            report.Content = request.Content;
            report.AccountId = request.AccountId;

            await _reportRepository.UpdateAsync(report);
            return _mapper.Map<ReportResponse>(report);
        }
        catch (Exception e)
        {
            throw new ServiceException(e.Message);
        }
    }

    public async Task<bool> UpdateStatus(int reportId, int status)
    {
        try
        {
            var report = await _reportRepository.GetByIdAsync(reportId);
            if (status != (int)ReportStatusEnum.Inactive && status != (int)ReportStatusEnum.Resolved)
                throw new ServiceException("Invalid status. Only 0 (Inactive) or 1 (Resolved) are allowed.");
            if (report.Status == (int)status)
                throw new ServiceException($"Report is already {status}.");
            if (report == null)
                throw new ServiceException("Report not found.");
            report.Status = status;
            await _reportRepository.UpdateAsync(report);
            return true;
        }
        catch (Exception e)
        {
            throw new ServiceException($"Error updating Report status: {e.Message}", e);
        }
    }
}