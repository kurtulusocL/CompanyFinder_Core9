using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyFormService
    {
        Task<IEnumerable<CompanyForm>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingAnswerableAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingNotAnswerableAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyForm>> GetAllIncludingByFormCategoryIdAsync(int formCategoryId);
        Task<IEnumerable<CompanyForm>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingForAdminByCompanyIdAsync(int? companyId);
        Task<CompanyForm> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image);
        Task<bool> UpdateAsync(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image, int id);
        Task<bool> DeleteAsync(CompanyForm entity, int id);
        Task<bool> SetAnswerable(int id);
        Task<bool> SetNotAnswerable(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<CompanyForm> GetAllSitemap();
        Task<IEnumerable<CompanyForm>> GetAllIncludingCompanyUserCompanyFormByCompanyIdAsync(int? companyId);
        IEnumerable<CompanyForm> GetAllIncludingRandom();
        Task<IEnumerable<CompanyForm>> GetAllIncludingSearchResultAsync(string key);
        Task<IEnumerable<CompanyForm>> GetAllIncludingMostLikedCompanyFormAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingMostHitCompanyFormAsync();
        Task<IEnumerable<CompanyForm>> GetAllIncludingYourMostLikedCompanyFormByUserIdAsync(string userId);
        Task<IEnumerable<CompanyForm>> GetAllIncludingYourMostHitCompanyFormByUserIdAsync(string userId);
        IEnumerable<CompanyForm> GetAllIncludingCompanyFormRandomForExplorerHome();
    }
}
