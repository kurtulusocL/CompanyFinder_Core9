namespace CompanyFinder.Business.Constants.Cache.CacheServices.CommentAnswerCache
{
    public  interface ICommentAnswerCacheService
    {
        Task CommentAnswerUpdateCache(int? commentId, string userId, int? id);
    }
}
