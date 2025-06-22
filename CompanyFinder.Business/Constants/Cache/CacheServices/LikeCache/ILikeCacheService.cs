namespace CompanyFinder.Business.Constants.Cache.CacheServices.LikeCache
{
    public interface ILikeCacheService
    {
        Task ClearAndUpdateCache();
        Task BlogCacheClear(int? blogId, string userId, int? id);
        Task CompanyCacheClear(int? companyId, string userId, int? id);
        Task ProductCacheClear(int? productId, string userId, int? id);
    }
}
