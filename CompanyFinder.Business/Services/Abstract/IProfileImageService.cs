using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IProfileImageService
    {
        Task<IEnumerable<ProfileImage>> GetAllIncludingAsync();
        Task<IEnumerable<ProfileImage>> GetAllIncludingAdminImageAsync();       
        Task<IEnumerable<ProfileImage>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<ProfileImage>> GetAllIncludingForAdmin();
        Task<IEnumerable<ProfileImage>> GetAllIncludingAdminImageForAdminAsync();      
        Task<IEnumerable<ProfileImage>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<ProfileImage> GetByIdAsync(int? id);
        ProfileImage GetProfileImageByUserId(string userId);
        Task<bool> CreateAsync(string userId, IFormFile image);
        Task<bool> DeleteAsync(ProfileImage entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<ProfileImage> GetAllIncludinggProfileImageForAdminHomeByUserId(string userId);
    }
}
