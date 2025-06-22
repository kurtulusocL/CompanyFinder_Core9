using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.CompanyCache
{
    public class CompanyCacheManager : ICompanyCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly ICompanyDal _companyDal;
        public CompanyCacheManager(IMemoryCache memoryCache, ICompanyDal companyDal)
        {
            _memoryCache = memoryCache;
            _companyDal = companyDal;
        }
        public async Task CompanyCacheUpdate(int? categoryId, int? subcategoryId, int? countryId, int? cityId, string? userId, int? id)
        {
            if (_memoryCache.TryGetValue(CompanyCacheKeys.GetAllCompanies, out List<Company> allCompanies))
            {
                var updatedCompanies = await _companyDal.GetAllAsync();
                _memoryCache.Set(CompanyCacheKeys.GetAllCompanies, updatedCompanies, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(CompanyCacheKeys.GetAllByFoundationDate, out List<Company> allCompaniesFoundation))
            {
                var updatedCompaniesFoundation = await _companyDal.GetAllAsync();
                _memoryCache.Set(CompanyCacheKeys.GetAllByFoundationDate, updatedCompaniesFoundation, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(CompanyCacheKeys.GetAllForAdmin, out List<Company> allCompaniesForAdmin))
            {
                var updatedCompaniesForAdmin = await _companyDal.GetAllAsync();
                _memoryCache.Set(CompanyCacheKeys.GetAllForAdmin, updatedCompaniesForAdmin, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(CompanyCacheKeys.GetAllCommentables, out List<Company> allCompaniesCommentable))
            {
                var updatedCompaniesCommentable = await _companyDal.GetAllAsync(i => i.IsCommentable);
                _memoryCache.Set(CompanyCacheKeys.GetAllCommentables, updatedCompaniesCommentable, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(CompanyCacheKeys.GetAllNotCommentables, out List<Company> allCompaniesNotCommentable))
            {
                var updatedCompaniesNotCommentable = await _companyDal.GetAllAsync(i => !i.IsCommentable);
                _memoryCache.Set(CompanyCacheKeys.GetAllNotCommentables, updatedCompaniesNotCommentable, TimeSpan.FromMinutes(25));
            }

            if (categoryId.HasValue)
            {
                string companyCategoryCacheKey = CompanyCacheKeys.GetAllIncludingByCategoryIdAsync(categoryId.Value);
                if (_memoryCache.TryGetValue(companyCategoryCacheKey, out List<Company> companyCategories))
                {
                    var updatedCategoryCompanies = await _companyDal.GetAllAsync(i => i.CompanyCategoryId == categoryId.Value);
                    _memoryCache.Set(companyCategoryCacheKey, updatedCategoryCompanies, TimeSpan.FromMinutes(25));
                }
            }

            if (subcategoryId.HasValue)
            {
                string companySubcategoryCacheKey = CompanyCacheKeys.GetAllIncludingBySubcategoryIdAsync(subcategoryId.Value);
                if (_memoryCache.TryGetValue(companySubcategoryCacheKey, out List<Company> companySubcategories))
                {
                    var updatedSubcategoryCompanies = await _companyDal.GetAllAsync(i => i.CompanySubcategoryId == subcategoryId.Value);
                    _memoryCache.Set(companySubcategoryCacheKey, updatedSubcategoryCompanies, TimeSpan.FromMinutes(25));
                }
            }

            if (countryId.HasValue)
            {
                string companyCountryCacheKey = CompanyCacheKeys.GetAllIncludingByCountryIdAsync(countryId.Value);
                if (_memoryCache.TryGetValue(companyCountryCacheKey, out List<Company> companyCountries))
                {
                    var updatedCountriesCompanies = await _companyDal.GetAllAsync(i => i.CountryId == countryId.Value);
                    _memoryCache.Set(companyCountryCacheKey, updatedCountriesCompanies, TimeSpan.FromMinutes(25));
                }
            }

            if (cityId.HasValue)
            {
                string companyCityCacheKey = CompanyCacheKeys.GetAllIncludingByCityIdAsync(cityId.Value);
                if (_memoryCache.TryGetValue(companyCityCacheKey, out List<Company> companyCities))
                {
                    var updatedCitiesCompanies = await _companyDal.GetAllAsync(i => i.CityId == cityId.Value);
                    _memoryCache.Set(companyCityCacheKey, updatedCitiesCompanies, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string companyUserCacheKey = CompanyCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(companyUserCacheKey, out List<Company> companyUsers))
                {
                    var updatedUserCompanies = await _companyDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(companyUserCacheKey, updatedUserCompanies, TimeSpan.FromMinutes(25));
                }

                string companyAdminUserCacheKey = CompanyCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(companyAdminUserCacheKey, out List<Company> companyAdminUsers))
                {
                    var updatedAdminUserCompanies = await _companyDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(companyAdminUserCacheKey, updatedAdminUserCompanies, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string companyCacheKey = CompanyCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(companyCacheKey, out Company singleCompany))
                {
                    var updatedCompany = await _companyDal.GetAsync(a => a.Id == id.Value);
                    if (updatedCompany != null)
                    {
                        _memoryCache.Set(companyCacheKey, updatedCompany, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
