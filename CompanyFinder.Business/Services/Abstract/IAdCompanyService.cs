using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAdCompanyService
    {
        Task<IEnumerable<AdCompany>> GetAllIncludingAsync();
        Task<IEnumerable<AdCompany>> GetAllIncludingForAddAdAsync();
        Task<IEnumerable<AdCompany>> GetAllIncludingForAdmin();
        Task<AdCompany> GetByIdAsync(int? id);
        Task<bool> CreateAsync(AdCompany entity);
        Task<bool> UpdateAsync(AdCompany entity);
        Task<bool> DeleteAsync(AdCompany entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
