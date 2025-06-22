using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllIncludingAsync();
        Task<IEnumerable<ProductCategory>> GetAllIncludingForAddProductAsync();
        Task<IEnumerable<ProductCategory>> GetAllIncludingForAddSubcategoryAsync();
        Task<IEnumerable<ProductCategory>> GetAllIncludingForAddCompanyPartnershipAsync();
        Task<IEnumerable<ProductCategory>> GetAllIncludingForAdmin();
        Task<ProductCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ProductCategory entity);
        Task<bool> UpdateAsync(ProductCategory entity);
        Task<bool> DeleteAsync(ProductCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<ProductCategory> GetAllProductCategoryForAdminHome();
        IEnumerable<ProductCategory> GetAllSiteMap();
        IEnumerable<ProductCategory> GetAllIncludingProductCategoriesForProductExplorer();
        IEnumerable<ProductCategory> GetAllIncludingProductCategoriesForProductAdvancedSearch();
        IEnumerable<ProductCategory> GetAllIncludingProductCategoryForCompanyPartnership();
    }
}
