using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;

namespace Service.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportResponse>> GetAllReports();
        Task<ReportResponse> GetReportsByAcountID(int acountID);
        Task CreateReport(CreateReportRequest request);
        Task<ReportResponse> UpdateReport(int acountID, UpdateReportRequest request);
        Task<Report> DeleteReport(int acountID);
    }
}
