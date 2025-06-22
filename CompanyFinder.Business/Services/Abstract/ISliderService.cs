using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<IEnumerable<Slider>> GetAllForAdmin();
        Task<Slider> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Slider entity, IFormFile image);
        Task<bool> UpdateAsync(Slider entity,IFormFile image);
        Task<bool> DeleteAsync(Slider entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Slider> GetAllSiteMap();
        IEnumerable<Slider> GetAllSlidersForHome();
    }
}
