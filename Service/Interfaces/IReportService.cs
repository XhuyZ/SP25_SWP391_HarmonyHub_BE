using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

namespace Service.Interfaces;
public interface IReportService
{
    Task<IEnumerable<ReportResponse>> GetAllReports();
    Task<ReportResponse> GetReportByID(int reportID);
    Task CreateReport(CreateReportRequest request);
    Task<ReportResponse> UpdateReport(int reportID, UpdateReportRequest request);
    Task<bool> UpdateStatus(int reportId, int status);
}
