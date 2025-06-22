using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllIncludingAsync();
        Task<IEnumerable<Product>> GetAllIncludingCommentableProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingNotCommentableProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingQuestionableProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingNotQuestionableProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingAvailableProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingNotAvailebleProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllIncludingBySubcategoryIdAsync(int? subcategoryId);
        Task<IEnumerable<Product>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Product>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingForAdmin();
        Task<IEnumerable<Product>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId);
        Task<IEnumerable<Product>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<Product> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string name, string desc, string? ortherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, IFormFile image);
        Task<bool> UpdateAsync(string name, string desc, string? ortherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, int id, IFormFile image);
        Task<bool> DeleteAsync(Product entity, int id);
        Task<List<SelectListItem>> ProductSelectSystem(int? productCategoryId, string tip);
        Task<bool> SetAvailableAsync(int id);
        Task<bool> SetNotAvailableAsync(int id);
        Task<bool> SetQuestionableAsync(int id);
        Task<bool> SetNotQuestionableAsync(int id);
        Task<bool> SetCommentableAsync(int id);
        Task<bool> SetNotCommentableAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Product> GetAllIncludingProductsForAdminHeader();
        int ProductCounter();
        IEnumerable<Product> GetAllSiteMap();
        IEnumerable<Product> GetAllIncludingCompanyProductsByCompanyId(int? companyId);
        Product GetIncludingProductForHeaderByCompanyId(int? companyId);
        Product GetIncludingProductByCompanyId(int? companyId);
        Product GetIncludingProductById(int? id);
        IEnumerable<Product> GetAllIncludingRandomProducts();
        Task<IEnumerable<Product>> GetAllIncludingSearchResultAsync(string key);
        Task<IEnumerable<Product>> GetAllIncludingProductAdvancedSearchResultAsync(string key);
        IEnumerable<Product> GetAllIncludingProductsForExplorer();
        IEnumerable<Product> GetAllIncludingLastProductsForExplorer();
        Task<IEnumerable<Product>> GetAllIncludingForPublicUserAsync();
        IEnumerable<Product> GetAllIncludingRandomProductForHome();
        Task<IEnumerable<Product>> GetAllIncludingMostLikedProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostHitProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostSavedProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostQuestionProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostCommentProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostExpensiveProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingMostCheapperProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingYourMostLikedProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostHitProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostSavedProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostQuestionProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostCommentProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostExpensiveProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingYourMostCheapperProductsAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingSavedCompanyProductsForSaveContentProductByUserIdAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingProductLikesForCompanyByUserIdAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingProductHitForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsForAdminAsync();
    }
}
