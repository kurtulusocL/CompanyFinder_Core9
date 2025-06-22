using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ISectorNewsService
    {
        Task<IEnumerable<SectorNews>> GetAllIncludingAsync();
        Task<IEnumerable<SectorNews>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<SectorNews>> GetAllIncludingMostLikedAsync();
        Task<IEnumerable<SectorNews>> GetAllIncludingMostHitAsync();
        Task<SectorNews> GetByIdAsync(int? id);
        Task<bool> CreateAsync(SectorNews entity, IFormFile image);
        Task<bool> UpdateAsync(SectorNews entity, IFormFile image);        
        Task<bool> DeleteAsync(SectorNews entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<SectorNews> GetAllSiteMap();
        IEnumerable<SectorNews> GetAllIncludingRandomSectorNews();
    }
}
