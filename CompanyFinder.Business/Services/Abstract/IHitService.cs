using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IHitService
    {
        Task<IEnumerable<Hit>> GetAllIncludingAsync();
        Task<IEnumerable<Hit>> GetAllIncludingByValueAsync();
        Task<IEnumerable<Hit>> GetAllIncludingByAdIdAsync(int? adId);
        Task<IEnumerable<Hit>> GetAllIncludingByBlogIdAsync(int? blogId);
        Task<IEnumerable<Hit>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Hit>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Hit>> GetAllIncludingByCompanyPartnershipIdAsync(int? companyPartnershipId);
        Task<IEnumerable<Hit>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId);
        Task<IEnumerable<Hit>> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId);
        Task<IEnumerable<Hit>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Hit>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<IEnumerable<Hit>> GetAllIncludingForAdmin();
        Task<Hit> GetByIdAsync(int? id);
        Hit AdHit(int? adId, string userId, int currentValue);
        Hit BlogHit(int? blogId, string userId, int currentValue);
        Hit ProductHit(int? productId, string userId, int currentValue);
        Hit CompanyHit(int? companyId, string userId, int currentValue);
        Hit CompanyPartnershipHit(int? companyPartnershipId, string userId, int currentValue);
        Hit CompanyFormHit(int? companyFormId, string userId, int currentValue);
        Hit SectorNewsHit(int? sectorNewsId, string userId, int currentValue);
        Task<bool> DeleteAsync(Hit entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<IEnumerable<Hit>> GetAllIncludingCompanyHitsPeopleByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Hit>> GetAllIncludingProductHitsPeopleByProductIdAsync(int? productId);
        Task<IEnumerable<Hit>> GetAllIncludingBlogHitsPeopleByBlogIdAsync(int? blogId);
        Task<IEnumerable<Hit>> GetAllIncludingPartnershipHitsPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId);
       
    }
}
