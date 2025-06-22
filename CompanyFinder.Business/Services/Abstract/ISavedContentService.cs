using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ISavedContentService
    {
        Task<IEnumerable<SavedContent>> GetAllIncludingAsync();
        Task<IEnumerable<SavedContent>> GetAllIncludingByNotSavedAsync();
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByCompanyIdAsync(int? companyId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByCompanyPartnershipIdAsync(int? companyPartnershipId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByProductIdAsync(int? productId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByBlogIdAsync(int? blogId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByUserIdAsync(string userId);
        Task<IEnumerable<SavedContent>> GetAllIncludingForAdmin();
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedByUserIdForAdminAsync(string userId);
        Task<SavedContent> GetByIdAsync(int? id);
        Task<bool> SaveAsync(bool isSaved, int? companyId, int? companyPartnershipId, int? productId, int? blogId, string userId);
        Task<bool> NotSaveAsync(bool isSaved, int? companyId, int? companyPartnershipId, int? productId, int? blogId, string userId);
        Task<bool> DeleteAsync(SavedContent entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedBlogContentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedCompanyContentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedProductContentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedPartnershipContentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedBlogsPeopleByBlogIdAsync(int? blogId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedCompaniesPeopleByCompanyIdAsync(int? companyId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedProductsPeopleByProductIdAsync(int? productId);
        Task<IEnumerable<SavedContent>> GetAllIncludingSavedPartnershipPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId);
    }
}
