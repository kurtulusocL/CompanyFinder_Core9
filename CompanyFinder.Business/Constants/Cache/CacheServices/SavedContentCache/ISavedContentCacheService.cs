namespace CompanyFinder.Business.Constants.Cache.CacheServices.SavedContentCache
{
    public interface ISavedContentCacheService
    {
        Task SavedContentCacheClear(int? companyId, int? productId, int? blogId, string userId, int? id);
    }
}
