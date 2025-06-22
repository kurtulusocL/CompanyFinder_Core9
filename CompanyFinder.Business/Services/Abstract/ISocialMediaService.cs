using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ISocialMediaService
    {
        Task<IEnumerable<SocialMedia>> GetAllAsync();
        Task<IEnumerable<SocialMedia>> GetAllForAdmin();
        Task<SocialMedia> GetByIdAsync(int? id);
        Task<bool> CreateAsync(SocialMedia entity, IFormFile image);
        Task<bool> UpdateAsync(SocialMedia entity, IFormFile image);
        Task<bool> DeleteAsync(SocialMedia entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<SocialMedia> GetAllSocialMediasForHomeFooter();
        IEnumerable<SocialMedia> GetAllSiteMap();
    }
}
