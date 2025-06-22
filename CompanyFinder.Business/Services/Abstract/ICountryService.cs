using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllIncludingAsync();
        Task<IEnumerable<Country>> GetAllIncludingForAddAdTargetAsync();
        Task<IEnumerable<Country>> GetAllIncludingForAddCompanyAsync();
        Task<IEnumerable<Country>> GetAllIncludingForAddCityAsync();
        Task<IEnumerable<Country>> GetAllIncludingForAdmin();
        Task<Country> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Country entity);
        Task<bool> UpdateAsync(Country entity);
        Task<bool> DeleteAsync(Country entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Country> GetAllSiteMap();
        IEnumerable<Country> GetAllIncludingCountriesForCompany();
        IEnumerable<Country> GetAllIncludingCountriesForCompanyAdvancedSearch();
    }
}
