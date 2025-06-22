using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAdTargetService
    {
        Task<IEnumerable<AdTarget>> GetAllIncludingAsync();
        Task<IEnumerable<AdTarget>> GetAllIncludingByCityIdAsync(int? cityId);
        Task<IEnumerable<AdTarget>> GetAllIncludingByCountryIdAsync(int? countryId);
        Task<IEnumerable<AdTarget>> GetAllIncludingByAdIdAsync(int? adId);
        Task<IEnumerable<AdTarget>> GetAllIncludingForAdmin();
        Task<AdTarget> GetByIdAsync(int? id);
        Task<bool> CreateAsync(int? cityId, int? countryId, int? adId);
        Task<bool> UpdateAsync(int? cityId, int? countryId, int? adId, int id);
        Task<bool> DeleteAsync(AdTarget entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
