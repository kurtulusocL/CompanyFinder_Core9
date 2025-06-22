using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanySubcategoryService
    {
        Task<IEnumerable<CompanySubcategory>> GetAllIncludingAsync();
        Task<IEnumerable<CompanySubcategory>> GetAllIncludingByCategoryIdAsync(int? categoryId);
        Task<IEnumerable<CompanySubcategory>> GetAllIncludingForAddCompanyByCategoryIdAsync(int? categoryId);
        Task<IEnumerable<CompanySubcategory>> GetAllIncludingForAdmin();
        Task<IEnumerable<CompanySubcategory>> GetAllIncludingCompanySubcategoriesForAdd();
        Task<CompanySubcategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CompanySubcategory entity);
        Task<bool> UpdateAsync(CompanySubcategory entity);
        Task<bool> DeleteAsync(CompanySubcategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<CompanySubcategory> GetAllCompanySubcategoryForAdminHome();
        IEnumerable<CompanySubcategory> GetAllSiteMap();
        IEnumerable<CompanySubcategory> GetAllIncludingCompanySubcategoriesForCompany();
        IEnumerable<CompanySubcategory> GetAllIncludingCompanySubcategoriesForCompanyAdvancedSearch();
    }
}
