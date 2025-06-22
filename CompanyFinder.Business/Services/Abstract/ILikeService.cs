using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ILikeService
    {
        Task<IEnumerable<Like>> GetAllIncludingAsync();
        Task<IEnumerable<Like>> GetAllIncludingByLikeValueAscAsync();
        Task<IEnumerable<Like>> GetAllIncludingByLikeValueDescAsync();
        Task<IEnumerable<Like>> GetAllIncludingByBlogIdAsync(int? blogId);
        Task<IEnumerable<Like>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Like>> GetAllIncludingByCompanyPartnershipIdAsync(int? companyPartnershipId);
        Task<IEnumerable<Like>> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId);
        Task<IEnumerable<Like>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId);
        Task<IEnumerable<Like>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Like>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Like>> GetAllIncludingByBlogIdForAdminAsync(int? blogId);
        Task<IEnumerable<Like>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId);
        Task<IEnumerable<Like>> GetAllIncludingByProductIdForAdminAsync(int? productId);
        Task<IEnumerable<Like>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<IEnumerable<Like>> GetAllIncludingForAdmin();
        Task<Like> GetByIdAsync(int? id);
        Task<bool> LikeBlogAsync(int? blogId, string userId, int currentValue);
        Task<bool> LikeCompanyAsync(int? companyId, string userId, int currentValue);
        Task<bool> LikeCompanyPartnershipAsync(int? companyPartnershipId, string userId, int currentValue);
        Task<bool> LikeCompanyFormAsync(int? companyFormId, string userId, int currentValue);
        Task<bool> LikeProductAsync(int? productId, string userId, int currentValue);
        Task<bool> LikeSectorNewsAsync(int? sectorNewsId, string userId, int currentValue);
        Task<bool> DeleteAsync(Like entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);  
        Task<IEnumerable<Like>> GetAllIncludingCompanyLikesPeopleByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Like>> GetAllIncludingProductLikesPeopleByProductIdAsync(int? productId);
        Task<IEnumerable<Like>> GetAllIncludingBlogLikesPeopleByBlogIdAsync(int? blogId);
        Task<IEnumerable<Like>> GetAllIncludingPartnershipLikesPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId);
    }
}
