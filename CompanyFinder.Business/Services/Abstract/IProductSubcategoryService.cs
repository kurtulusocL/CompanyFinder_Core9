using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IProductSubcategoryService
    {
        Task<IEnumerable<ProductSubcategory>> GetAllIncludingAsync();
        Task<IEnumerable<ProductSubcategory>> GetAllIncludingByCategoryIdAsync(int? categoryId);
        Task<IEnumerable<ProductSubcategory>> GetAllIncludingForAddProductByCategoryIdAsync(int? categoryId);
        Task<IEnumerable<ProductSubcategory>> GetAllIncludingForAdmin();
        Task<ProductSubcategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ProductSubcategory entity);
        Task<bool> UpdateAsync(ProductSubcategory entity);
        Task<bool> DeleteAsync(ProductSubcategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<ProductSubcategory> GetAllProductSubcategoriesForAdminHome();
        IEnumerable<ProductSubcategory> GetAllSiteMap();
        IEnumerable<ProductSubcategory> GetAllIncludingProductSubcategoriesForProductExplorer();
        IEnumerable<ProductSubcategory> GetAllIncludingProductSubcategoriesForProductAdvancedSearch();
    }
}
