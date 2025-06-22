using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyCategoryService
    {
        Task<IEnumerable<CompanyCategory>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyCategory>> GetAllIncludingForAddSubcategoryAsync();
        Task<IEnumerable<CompanyCategory>> GetAllIncludingForAddCompanyAsync();
        Task<IEnumerable<CompanyCategory>> GetAllIncludingForAdmin();
        Task<CompanyCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CompanyCategory entity);
        Task<bool> UpdateAsync(CompanyCategory entity);
        Task<bool> DeleteAsync(CompanyCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<CompanyCategory> GetAllCompanyCategoriesForAdminHome();
        IEnumerable<CompanyCategory> GetAllSiteMap();
        IEnumerable<CompanyCategory> GetAllIncludingCompanyCategoriesForCompany();
        IEnumerable<CompanyCategory> GetAllIncludingCompanyCategoriesForCompanyAdvancedSearch();
    }
}
