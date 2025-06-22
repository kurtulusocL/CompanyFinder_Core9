
namespace CompanyFinder.Business.Constants.Cache.CacheServices.CompanyCache
{
    public interface ICompanyCacheService
    {
        Task CompanyCacheUpdate(int? categoryId, int? subcategoryId, int? countryId, int? cityId, string? userId, int? id);
    }
}
