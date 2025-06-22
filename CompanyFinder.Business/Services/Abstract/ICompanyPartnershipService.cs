using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyPartnershipService
    {
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByStartDateAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByExpiryDateAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingNotAvailableAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingAvailableAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByProductCategoryIdAsync(int productCategoryId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingForAdminAsync();
        Task<CompanyPartnership> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, int? companyId, int productCategoryId, string userId);
        Task<bool> UpdateAsync(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, bool isAvailable, int? companyId, int productCategoryId, string userId, int id);
        Task<bool> DeleteAsync(CompanyPartnership entity, int id);
        Task<bool> SetAvailablePartnership(int id);
        Task<bool> SetNotAvailablePartnership(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingSavedPartnershipsForSaveContentPartnershipByUserIdAsync(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingPartnershipLikesforCompanyByUserId(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingPartnershipHitsforCompanyByUserId(string userId);
        IEnumerable<CompanyPartnership> GetAllIncludingCompanyPartnershipRandom();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingSearchResultasync(string key);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingAdvancedSearchResultAsync(string key);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostLikedPartnershipAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostHitPartnershipAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostSavedPartnershipAsync();
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostLikedPartnershipByUserIdAsync(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostHitPartnershipByUserIdAsync(string userId);
        Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostSavedPartnershipByUserIdAsync(string userId);
    }
}
