using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ILayoutInfoService
    {
        Task<IEnumerable<LayoutInfo>> GetAllAsync();
        Task<IEnumerable<LayoutInfo>> GetAllForAdmin();
        Task<LayoutInfo> GetByIdAsync(int? id);
        Task<bool> CreateAsync(LayoutInfo entity, IFormFile image);
        Task<bool> UpdateAsync(LayoutInfo entity, IFormFile image);
        Task<bool> DeleteAsync(LayoutInfo entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<LayoutInfo> GetAllLayoutInfo();
        IEnumerable<LayoutInfo> GetAllSiteMap();
    }
}
