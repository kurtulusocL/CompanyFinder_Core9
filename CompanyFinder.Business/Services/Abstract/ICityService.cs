using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllIncludingAsync();
        Task<IEnumerable<City>> GetAllIncludingForAddCompanyAsync(int? countryId);
        Task<IEnumerable<City>> GetAllIncludingByCountryIdAsync(int? countryId);
        Task<IEnumerable<City>> GetAllIncludingCompanyCitiesForAdd();
        Task<IEnumerable<City>> GetAllIncludingForAdmin();
        Task<City> GetByIdAsync(int? id);
        Task<bool> CreateAsync(City entity);
        Task<bool> UpdateAsync(City entity);
        Task<bool> DeleteAsync(City entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<City> GetAllCityForAdminHome();
        IEnumerable<City> GetAllSiteMap();
        IEnumerable<City> GetAllIncludingCitiesForCompanies();
        IEnumerable<City> GetAllIncludingCitiesForCompanyAdvancedSearch();
        Task<IEnumerable<City>> GetAllIncludingCitiesForAddAdTargetAsync();
        int CityCounter();
    }
}