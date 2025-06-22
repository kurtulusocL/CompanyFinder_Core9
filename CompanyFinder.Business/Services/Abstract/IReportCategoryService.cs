using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IReportCategoryService
    {
        Task<IEnumerable<ReportCategory>> GetAllIncludingAsync();
        Task<IEnumerable<ReportCategory>> GetAllIncludingForAddReportAsync();
        Task<IEnumerable<ReportCategory>> GetAllIncludingForAdmin();
        Task<ReportCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ReportCategory entity);
        Task<bool> UpdateAsync(ReportCategory entity);
        Task<bool> DeleteAsync(ReportCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
