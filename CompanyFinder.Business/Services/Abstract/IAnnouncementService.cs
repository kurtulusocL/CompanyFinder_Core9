using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAsync();
        Task<IEnumerable<Announcement>> GetAllAdminAnnouncementAsync();
        Task<IEnumerable<Announcement>> GetAllUserAnnouncementAsync();
        Task<IEnumerable<Announcement>> GetAllAnnouncementForEverybodyAsync();
        Task<IEnumerable<Announcement>> GetAllForAdmin();
        Task<Announcement> GetByIdAsync(int? id);
        Announcement HitRead(int? id);
        Task<bool> CreateAsync(Announcement entity, IFormFile image);
        Task<bool> UpdateAsync(Announcement entity, IFormFile image);
        Task<bool> DeleteAsync(Announcement entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
