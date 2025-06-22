using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAdService
    {
        Task<IEnumerable<Ad>> GetAllIncludingAsync();
        Task<IEnumerable<Ad>> GetAllIncludingAdsNoTargetAsync();
        Task<IEnumerable<Ad>> GetAllIncludingAdsByTargetAsync();
        Task<IEnumerable<Ad>> GetAllIncludingByExpiryDateAsync();
        Task<IEnumerable<Ad>> GetAllIncludingByCompanyIdAsync(int companyId);
        Task<IEnumerable<Ad>> GetAllIncludingForAdmin();
        Task<Ad> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Ad entity, IFormFile image);
        Task<bool> UpdateAsync(Ad entity, IFormFile image);
        Task<bool> DeleteAsync(Ad entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Ad SimilarHitRead(int? id);
        int AdCounter();
        IEnumerable<Ad> GetAllIncludingRandomAd();
        IEnumerable<Ad> GetAllIncludingRandomAdForHome();
    }
}
