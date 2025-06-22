using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyMessageService
    {
        Task<IEnumerable<CompanyMessage>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyMessage>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyMessage>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<CompanyMessage>> GetAllIncludingForAdmin();
        Task<CompanyMessage> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string? title, string subject, string desc, int? companyId, string userId);
        Task<bool> UpdateAsync(string? title, string subject, string desc, int? companyId, string userId, int id);
        Task<bool> DeleteAsync(CompanyMessage entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
