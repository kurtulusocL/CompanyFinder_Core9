using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.CommentAnswerCache
{
    public class CommentAnswerCacheManager : ICommentAnswerCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly ICommentAnswerDal _commentAnswerDal;
        public CommentAnswerCacheManager(IMemoryCache memoryCache, ICommentAnswerDal commentAnswerDal)
        {
            _memoryCache = memoryCache;
            _commentAnswerDal = commentAnswerDal;
        }
        public async Task CommentAnswerUpdateCache(int? commentId, string userId, int? id)
        {
            if (_memoryCache.TryGetValue(CommentAnswerCacheKeys.AllCommentAnswers, out List<CommentAnswer> allBlogs))
            {
                var updatedCommentAnswers = await _commentAnswerDal.GetAllAsync();
                _memoryCache.Set(CommentAnswerCacheKeys.AllCommentAnswers, updatedCommentAnswers, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(CommentAnswerCacheKeys.AllCommentAnswerForAdmin, out List<CommentAnswer> adminBlogs))
            {
                var updatedCommentAnswers = await _commentAnswerDal.GetAllAsync();
                _memoryCache.Set(CommentAnswerCacheKeys.AllCommentAnswerForAdmin, updatedCommentAnswers, TimeSpan.FromMinutes(25));
            }

            if (commentId.HasValue)
            {
                string commentAnswerCacheKey = CommentAnswerCacheKeys.GetAllIncludingByCommentIdAsync(commentId.Value);
                if (_memoryCache.TryGetValue(commentAnswerCacheKey, out List<CommentAnswer> commentAnswers))
                {
                    var updatedCommentAnswers = await _commentAnswerDal.GetAllAsync(i => i.CommentId == commentId.Value);
                    _memoryCache.Set(commentAnswerCacheKey, updatedCommentAnswers, TimeSpan.FromMinutes(25));
                }

                string commentAnswerForAdminCacheKey = CommentAnswerCacheKeys.GetAllIncludingForAdminByCommentIdAsync(commentId.Value);
                if (_memoryCache.TryGetValue(commentAnswerCacheKey, out List<CommentAnswer> commentAnswersForAdmin))
                {
                    var updatedCommentAnswers = await _commentAnswerDal.GetAllAsync(i => i.CommentId == commentId.Value);
                    _memoryCache.Set(commentAnswerForAdminCacheKey, updatedCommentAnswers, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = CommentAnswerCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<CommentAnswer> userAppointments))
                {
                    var updatedUserBlogs = await _commentAnswerDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = CommentAnswerCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<CommentAnswer> userAdminBlogs))
                {
                    var updatedUserAdminAnswers = await _commentAnswerDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminAnswers, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string commentAnswerCacheKey = CommentAnswerCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(commentAnswerCacheKey, out Blog singleBlog))
                {
                    var updatedCommentAnswer = await _commentAnswerDal.GetAsync(a => a.Id == id.Value);
                    if (updatedCommentAnswer != null)
                    {
                        _memoryCache.Set(commentAnswerCacheKey, updatedCommentAnswer, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
