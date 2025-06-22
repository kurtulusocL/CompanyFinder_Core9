using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CityManager : ICityService
    {
        readonly ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public int CityCounter()
        {
            var result = _cityDal.CityCounter();
            return result;
        }

        public async Task<bool> CreateAsync(City entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _cityDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(City entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _cityDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _cityDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IEnumerable<City> GetAllCityForAdminHome()
        {
            try
            {
                return _cityDal.GetAllInclude(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies, y => y.Country).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _cityDal.GetAllIncludeAsync(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Country, y => y.AdTargets);
                return result.OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingByCountryIdAsync(int? countryId)
        {
            try
            {
                if (countryId == null)
                    throw new ArgumentNullException(nameof(countryId), "CountryId was null");

                var result = await _cityDal.GetAllIncludeByIdAsync(countryId, "CountryId",
                    new Expression<Func<City, bool>>[]
                    {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                    },
                    i => i.AdTargets,
                    i => i.Companies,
                    i => i.Country
                );
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingCitiesForAddAdTargetAsync()
        {
            try
            {
                var result = await _cityDal.GetAllIncludeAsync(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country);
                return result.OrderByDescending(i => i.AdTargets.Count()).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IEnumerable<City> GetAllIncludingCitiesForCompanies()
        {
            try
            {
                return _cityDal.GetAllInclude(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies, y => y.Country).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public IEnumerable<City> GetAllIncludingCitiesForCompanyAdvancedSearch()
        {
            try
            {
                return _cityDal.GetAllInclude(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null).OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingCompanyCitiesForAdd()
        {
            try
            {
                var result = await _cityDal.GetAllIncludeAsync(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.Country);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingForAddCompanyAsync(int? countryId)
        {
            try
            {
                if (countryId == null)
                    throw new ArgumentNullException(nameof(countryId), "CountryId was null");

                var result = await _cityDal.GetAllIncludeByIdAsync(countryId, "CountryId",
                    new Expression<Func<City, bool>>[]
                    {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                    }, null,
                    i => i.AdTargets,
                    i => i.Companies,
                    i => i.Country
                );
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<IEnumerable<City>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _cityDal.GetAllIncludeAsync(new Expression<Func<City, bool>>[]
                {

                }, null, y => y.Companies, y => y.Country, y => y.AdTargets);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public IEnumerable<City> GetAllSiteMap()
        {
            try
            {
                return _cityDal.GetAllInclude(new Expression<Func<City, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<City>();
            }
        }

        public async Task<City> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _cityDal.GetIncludeAsync(i => i.Id == id, y => y.Companies, y => y.Country, y => y.AdTargets);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _cityDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _cityDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _cityDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _cityDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(City entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _cityDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
