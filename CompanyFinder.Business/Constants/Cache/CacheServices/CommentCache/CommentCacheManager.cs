using System.ComponentModel.Design;
using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.CommentCache
{
    public class CommentCacheManager : ICommentCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly ICommentDal _commentDal;
        public CommentCacheManager(IMemoryCache memoryCache, ICommentDal commentDal)
        {
            _memoryCache = memoryCache;
            _commentDal = commentDal;
        }

        public async Task BlogCommentUpdateCache(int? blogId, string userId, int? id)
        { 
            if (blogId.HasValue)
            {
                string commentCacheKey = CommentCacheKeys.GetAllIncludingByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(commentCacheKey, out List<Comment> commentBlogs))
                {
                    var updatedCommentCaches = await _commentDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(commentCacheKey, updatedCommentCaches, TimeSpan.FromMinutes(25));
                }

                string commentCacheKeys = CommentCacheKeys.GetAllIncludingByBlogIdForAdminAsync(blogId.Value);
                if (_memoryCache.TryGetValue(commentCacheKey, out List<Comment> commentBlogsForAdmin))
                {
                    var updatedCommentCache = await _commentDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(commentCacheKeys, updatedCommentCache, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = CommentCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Comment> userBlogs))
                {
                    var updatedUserComments = await _commentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserComments, TimeSpan.FromMinutes(30));
                }

                string userAdminCacheKey = CommentCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Comment> userAdminBlogs))
                {
                    var updatedUserAdminComments = await _commentDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminComments, TimeSpan.FromMinutes(30));
                }
            }

            if (id.HasValue)
            {
                string commentCacheKey = CommentCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(commentCacheKey, out Comment singleComment))
                {
                    var updatedComment = await _commentDal.GetAsync(a => a.Id == id.Value);
                    if (updatedComment != null)
                    {
                        _memoryCache.Set(commentCacheKey, updatedComment, TimeSpan.FromMinutes(30));
                    }
                }
            }
        }

        public async Task CommentUpdateCache()
        {
            if (_memoryCache.TryGetValue(CommentCacheKeys.GetAllComments, out List<Comment> allComments))
            {
                var updatedComments = await _commentDal.GetAllAsync();
                _memoryCache.Set(CommentCacheKeys.GetAllComments, updatedComments, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(CommentCacheKeys.GetAllCommentsForAdmin, out List<Comment> allCommentsForAdmin))
            {
                var updatedComments = await _commentDal.GetAllAsync();
                _memoryCache.Set(CommentCacheKeys.GetAllCommentsForAdmin, updatedComments, TimeSpan.FromMinutes(25));
            }
        }

        public async Task CompanyUpdateCache(int? companyId, string userId, int? id)
        {            
            if (companyId.HasValue)
            {
                string companyCacheKey = CommentCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyCacheKey, out List<Comment> companyComments))
                {
                    var updatedCompanyComments = await _commentDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyCacheKey, updatedCompanyComments, TimeSpan.FromMinutes(25));
                }

                string companyCacheKeys = CommentCacheKeys.GetAllIncludingByCompanyIdForAdminAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyCacheKeys, out List<Comment> companyCommentsForAdmin))
                {
                    var updatedCompanyComment = await _commentDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyCacheKeys, updatedCompanyComment, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = CommentCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Comment> userBlogs))
                {
                    var updatedUserComments = await _commentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserComments, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = CommentCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Comment> userAdminBlogs))
                {
                    var updatedUserAdminComments = await _commentDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminComments, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string commentCacheKey = CommentCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(commentCacheKey, out Comment singleComment))
                {
                    var updatedComment = await _commentDal.GetAsync(a => a.Id == id.Value);
                    if (updatedComment != null)
                    {
                        _memoryCache.Set(commentCacheKey, updatedComment, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ProductUpdateCache(int? productId, string userId, int? id)
        {
            if (productId.HasValue)
            {
                string productCacheKey = CommentCacheKeys.GetAllIncludingByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productCacheKey, out List<Comment> productComments))
                {
                    var updatedProductComments = await _commentDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productCacheKey, updatedProductComments, TimeSpan.FromMinutes(25));
                }

                string productCacheKeysForAdmin = CommentCacheKeys.GetAllIncludingByProductIdForAdminAsync(productId.Value);
                if (_memoryCache.TryGetValue(productCacheKeysForAdmin, out List<Comment> productCommentsForAdmin))
                {
                    var updatedProductComment = await _commentDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productCacheKeysForAdmin, updatedProductComment, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = CommentCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Comment> userBlogs))
                {
                    var updatedUserComments = await _commentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserComments, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = CommentCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Comment> userAdminBlogs))
                {
                    var updatedUserAdminComments = await _commentDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminComments, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string commentCacheKey = CommentCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(commentCacheKey, out Comment singleComment))
                {
                    var updatedComment = await _commentDal.GetAsync(a => a.Id == id.Value);
                    if (updatedComment != null)
                    {
                        _memoryCache.Set(commentCacheKey, updatedComment, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
