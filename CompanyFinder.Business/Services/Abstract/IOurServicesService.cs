using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IOurServicesService
    {
        Task<IEnumerable<OurServices>> GetAllAsync();
        Task<IEnumerable<OurServices>> GetAllForAdmin();
        Task<OurServices> GetByIdAsync(int? id);
        Task<bool> CreateAsync(OurServices entity, IFormFile image);
        Task<bool> UpdateAsync(OurServices entity, IFormFile image);
        Task<bool> DeleteAsync(OurServices entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<OurServices> GetAllSiteMap();
        IEnumerable<OurServices> GetAllOurServicesRandomForAbout();
        IEnumerable<OurServices> GetAllOurServicesRandomForHome();
        IEnumerable<OurServices> GetAllOurServicesForHome();
    }
}
