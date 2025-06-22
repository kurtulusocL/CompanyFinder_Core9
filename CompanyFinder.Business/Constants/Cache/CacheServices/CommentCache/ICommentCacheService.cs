namespace CompanyFinder.Business.Constants.Cache.CacheServices.CommentCache
{
    public interface ICommentCacheService
    {
        Task CommentUpdateCache();
        Task BlogCommentUpdateCache(int? blogId, string userId, int? id);
        Task CompanyUpdateCache(int? companyId, string userId, int? id);
        Task ProductUpdateCache(int? productId, string userId, int? id);
    }
}
