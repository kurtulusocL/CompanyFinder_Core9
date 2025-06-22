using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAboutService
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task<IEnumerable<About>> GetAllForAdmin();
        Task<About> GetByIdAsync(int? id);
        About HitRead(int? id);
        Task<bool> CreateAsync(About entity, IFormFile image);
        Task<bool> UpdateAsync(About entity, IFormFile image);
        Task<bool> DeleteAsync(About entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<About> GetAllAboutForFooter();
        IEnumerable<About> GetAllSiteMap();
        IEnumerable<About> GetAllAboutForHome();
    }
}
