using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<IEnumerable<News>> GetAllForAdmin();
        Task<News> GetByIdAsync(int? id);
        Task<bool> LikeAsync(int? id);
        News HitRead(int? id);
        Task<bool> CreateAsync(News entity, IFormFile image);
        Task<bool> UpdateAsync(News entity, IFormFile image);
        Task<bool> DeleteAsync(News entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<News> GetAllNewsForFooter();
        IEnumerable<News> GetAllSiteMap();
        IEnumerable<News> GetAllNewsForExplorerNewsDetail();
    }
}
