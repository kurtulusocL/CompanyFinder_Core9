namespace CompanyFinder.Business.Constants.Cache.CacheServices.QuestionCache
{
    public interface IQuestionCacheService
    {
        Task ClearUpdateCache();
        Task CompanyCacheClear(int? companyId, string userId, int? id);
        Task ProductCacheClear(int? productId, string userId, int? id);
    }
}
