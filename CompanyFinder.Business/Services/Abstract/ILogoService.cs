using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ILogoService
    {
        Task<IEnumerable<Logo>> GetAllAsync();
        Task<IEnumerable<Logo>> GetAllForAdmin();
        Task<Logo> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Logo entity, IFormFile image);
        Task<bool> UpdateAsync(Logo entity, IFormFile image);
        Task<bool> DeleteAsync(Logo entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Logo> GetAllLogoForIcon();
        IEnumerable<Logo> GetAllLogoForHome();
        IEnumerable<Logo> GetAllSiteMap();
    }
}
