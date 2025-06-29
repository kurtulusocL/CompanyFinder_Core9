using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CountryManager : ICountryService
    {
        readonly ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public async Task<bool> CreateAsync(Country entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _countryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Country entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _countryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _countryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Country>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _countryDal.GetAllIncludeAsync(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public IEnumerable<Country> GetAllIncludingCountriesForCompany()
        {
            try
            {
                return _countryDal.GetAllInclude(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false,
                i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public IEnumerable<Country> GetAllIncludingCountriesForCompanyAdvancedSearch()
        {
            try
            {
                return _countryDal.GetAllInclude(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false,
                i=>i.Companies.Count()>0
                }, null).OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<Country>> GetAllIncludingForAddAdTargetAsync()
        {
            try
            {
                var result = await _countryDal.GetAllIncludeAsync(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.AdTargets.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<Country>> GetAllIncludingForAddCityAsync()
        {
            try
            {
                var result = await _countryDal.GetAllIncludeAsync(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.Cities.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<Country>> GetAllIncludingForAddCompanyAsync()
        {
            try
            {
                var result = await _countryDal.GetAllIncludeAsync(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public async Task<IEnumerable<Country>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _countryDal.GetAllIncludeAsync(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public IEnumerable<Country> GetAllSiteMap()
        {
            try
            {
                var result = _countryDal.GetAllInclude(new Expression<Func<Country, bool>>[] {
                i=>i.IsActive==true,
                y=>y.IsDeleted==false
                }, null, y => y.Companies, y => y.Cities, y => y.AdTargets);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Country>();
            }
        }

        public async Task<Country> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _countryDal.GetIncludeAsync(i => i.Id == id, y => y.Companies, y => y.Cities, y => y.AdTargets);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _countryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _countryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _countryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _countryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Country entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _countryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
