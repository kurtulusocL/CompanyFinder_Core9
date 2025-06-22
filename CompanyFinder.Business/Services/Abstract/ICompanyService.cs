using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllIncludingAsync();
        Task<IEnumerable<Company>> GetAllIncludingByFoundationDateAsync();
        Task<IEnumerable<Company>> GetAllIncludingByCommentablesAsync();
        Task<IEnumerable<Company>> GetAllIncludingByNotCommentablesAsync();
        Task<IEnumerable<Company>> GetAllIncludingByCountryIdAsync(int countryId);
        Task<IEnumerable<Company>> GetAllIncludingByCityIdAsync(int? cityId);
        Task<IEnumerable<Company>> GetAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Company>> GetAllIncludingBySubcategoryIdAsync(int? subcategoryId);
        Task<IEnumerable<Company>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingForAdmin();
        Task<IEnumerable<Company>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<Company> GetByIdAsync(int? id);
        Company GetCompanyLogo(string userId);
        Task<bool> CreateAsync(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, IFormFile image);
        Task<bool> UpdateAsync(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, int id, IFormFile image);
        Task<bool> DeleteAsync(Company entity, int id);
        Task<List<SelectListItem>> LocationSelectSystem(int? companyCountryId, string tip);
        Task<List<SelectListItem>> CategorySelectSystem(int? companyCategoryId, string tip);        
        Task<bool> SetCommentableAsync(int id);
        Task<bool> SetNotCommentableAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Company> GetAllIncludingLastCompaniesForAdminHome();
        IEnumerable<Company> GetAllIncludingLastCompaniesForAdminHeader();
        int CompanyCounter();
        Company GetIncludingCompaniesByUserId(string userId);
        Company GetIncludeCompanyForCompanyUserLogoByUserId(string userId);
        Task<Company> GetIncludingCompanyForCompanyUserByIdAsync(int? id);
        Task<IEnumerable<Company>> GetAllIncludingCompaniesForCompanyUserAsync();
        IEnumerable<Company> GetAllIncludingRandomCompanies();
        Company GetCompanyById(int? id);
        Task<IEnumerable<Company>> GetAllIncludingSearchResultAsync(string key);
        Task<IEnumerable<Company>> GetAllIncludingCompanyAdvancedSearchResultAsync(string key);
        Task<Company> GetCompanyByUserIdAsync(string userId);
        IEnumerable<Company> GetAllIncludingCompaniesForExplorer();
        IEnumerable<Company> GetAllIncludingLastCompaniesForExplorer();
        Company GetIncludingCompanyForHeaderByUserId(string userId);
        Company GetIncludingCompanyForCompanyUserCounter(string userId);
        Task<IEnumerable<Company>> GetAllIncludingMostLikedCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingMostHitCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingMostSavedCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingMostAppointmentCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingMostCommentCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingYourMostLikedCompaniesAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingYourMostHitCompaniesAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingYourMostSavedCompaniesAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingYourMostAppointmentCompaniesAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingYourMostCommentCompaniesAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingCompanyMatchResultsAsync(string key);
        Company GetCompanyLogoForCommentByUserId(string userId);
        Task<IEnumerable<Company>> GetAllIncludingCompanyLikesForCompanyByUserIdAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingCompanyHitForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingSavedCompaniesForSaveContentCompanyByUserIdAsync(string userId);
        Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesFortAdminAsync();
        Company GetCompanyInformationForCommentByUserId(string userId);
    }
}