using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IReportAnswerService
    {
        Task<IEnumerable<ReportAnswer>> GetAllIncludingAsync();
        Task<IEnumerable<ReportAnswer>> GetAllIncludingByReportIdAsync(int? reportId);
        Task<IEnumerable<ReportAnswer>> GetAllIncludingSatisfieldsAsync();
        Task<IEnumerable<ReportAnswer>> GetAllIncludingNotSatisfieldsAsync();
        Task<IEnumerable<ReportAnswer>> GetAllIncludingForAdmin();
        Task<IEnumerable<ReportAnswer>> GetAllIncludingForAdminByReportIdAsync(int? reportId);
        Task<ReportAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string desc, int? reportId);
        Task<bool> UpdateAsync(string title, string desc, int? reportId, int id);
        Task<bool> DeleteAsync(ReportAnswer entity, int id);
        Task<bool> SetSatisfieldAsync(int id);
        Task<bool> SetNotSatisfieldAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int ReportAnswerCounter();
        IEnumerable<ReportAnswer> GetAllIncludingReportAnswerForCompanyUserByReportId(int? reportId);
    }
}
