namespace CompanyFinder.Business.Constants.Cache.CacheServices.HitCache
{
    public interface IHitCacheService
    {
        void ClearUpdateHitCaches();
        void AdHitCacheUpdate(int? adId, string? userId, int? id);
        void BlogHitCacheUpdate(int? blogId, string? userId, int? id);
        void CompanyHitCacheUpdate(int? companyId, string? userId, int? id);
        void ProductHitCacheUpdate(int? productId, string? userId, int? id);
    }
}
