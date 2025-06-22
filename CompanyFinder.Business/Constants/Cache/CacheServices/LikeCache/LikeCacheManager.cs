using System.ComponentModel.Design;
using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.LikeCache
{
    public class LikeCacheManager : ILikeCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly ILikeDal _likeDal;
        public LikeCacheManager(IMemoryCache memoryCache, ILikeDal likeDal)
        {
            _memoryCache = memoryCache;
            _likeDal = likeDal;
        }

        public async Task ClearAndUpdateCache()
        {
            if (_memoryCache.TryGetValue(LikeCacheKeys.GetAllLikes, out List<Like> allLikes))
            {
                var updatedLikes = await _likeDal.GetAllAsync();
                _memoryCache.Set(LikeCacheKeys.GetAllLikes, updatedLikes, TimeSpan.FromMinutes(25));
            }
            
            if (_memoryCache.TryGetValue(LikeCacheKeys.GetAllLikesValueAsc, out List<Like> allLikesByValueAsc))
            {
                var updatedLikesByValueAsc = await _likeDal.GetAllAsync();
                _memoryCache.Set(LikeCacheKeys.GetAllLikesValueAsc, updatedLikesByValueAsc, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(LikeCacheKeys.GetAllLikesValueDesc, out List<Like> allLikesByValueDesc))
            {
                var updatedLikesByValueDesc = await _likeDal.GetAllAsync();
                _memoryCache.Set(LikeCacheKeys.GetAllLikesValueDesc, updatedLikesByValueDesc, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(LikeCacheKeys.GetAllLikesForAdmin, out List<Like> allLikesForAdmin))
            {
                var updatedLikesForAdmin = await _likeDal.GetAllAsync();
                _memoryCache.Set(LikeCacheKeys.GetAllLikesForAdmin, updatedLikesForAdmin, TimeSpan.FromMinutes(25));
            }
        }

        public async Task BlogCacheClear(int? blogId, string userId, int? id)
        {
            if (blogId.HasValue)
            {
                string blogLikeCacheKey = LikeCacheKeys.GetAllIncludingByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogLikeCacheKey, out List<Like> blogLikes))
                {
                    var updatedBlogLikeCacheKeys = await _likeDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogLikeCacheKey, updatedBlogLikeCacheKeys, TimeSpan.FromMinutes(25));
                }

                string blogAdminLikeCacheKey = LikeCacheKeys.GetAllIncludingByBlogIdForAdminAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogAdminLikeCacheKey, out List<Like> blogAdminLikes))
                {
                    var updatedAdminBlogLikeCacheKeys = await _likeDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogAdminLikeCacheKey, updatedAdminBlogLikeCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = LikeCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Like> userHits))
                {
                    var updatedUserLikes = await _likeDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserLikes, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = LikeCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Like> userAdminLikes))
                {
                    var updatedUserAdminLikes = await _likeDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminLikes, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string likeCacheKey = LikeCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(likeCacheKey, out Like singleLike))
                {
                    var updatedLike = await _likeDal.GetAsync(a => a.Id == id.Value);
                    if (updatedLike != null)
                    {
                        _memoryCache.Set(likeCacheKey, updatedLike, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }       

        public async Task CompanyCacheClear(int? companyId, string userId, int? id)
        {
            if (companyId.HasValue)
            {
                string companyLikeCacheKey = LikeCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyLikeCacheKey, out List<Like> companyLikes))
                {
                    var updatedCompanyLikeCacheKeys = await _likeDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyLikeCacheKey, updatedCompanyLikeCacheKeys, TimeSpan.FromMinutes(25));
                }

                string productAdminLikeCacheKey = LikeCacheKeys.GetAllIncludingByProductIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(productAdminLikeCacheKey, out List<Like> companyAdminLikes))
                {
                    var updatedAdminCompanyLikeCacheKeys = await _likeDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(productAdminLikeCacheKey, updatedAdminCompanyLikeCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = LikeCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Like> userHits))
                {
                    var updatedUserLikes = await _likeDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserLikes, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = LikeCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Like> userAdminLikes))
                {
                    var updatedUserAdminLikes = await _likeDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminLikes, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string likeCacheKey = LikeCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(likeCacheKey, out Like singleLike))
                {
                    var updatedLike = await _likeDal.GetAsync(a => a.Id == id.Value);
                    if (updatedLike != null)
                    {
                        _memoryCache.Set(likeCacheKey, updatedLike, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ProductCacheClear(int? productId, string userId, int? id)
        {
            if (productId.HasValue)
            {
                string productLikeCacheKey = LikeCacheKeys.GetAllIncludingByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productLikeCacheKey, out List<Like> productLikes))
                {
                    var updatedProductLikeCacheKeys = await _likeDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productLikeCacheKey, updatedProductLikeCacheKeys, TimeSpan.FromMinutes(25));
                }

                string productAdminLikeCacheKey = LikeCacheKeys.GetAllIncludingByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productAdminLikeCacheKey, out List<Like> productAdminLikes))
                {
                    var updatedAdminProductLikeCacheKeys = await _likeDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productAdminLikeCacheKey, updatedAdminProductLikeCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = LikeCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Like> userHits))
                {
                    var updatedUserLikes = await _likeDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserLikes, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = LikeCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Like> userAdminLikes))
                {
                    var updatedUserAdminLikes = await _likeDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminLikes, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string likeCacheKey = LikeCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(likeCacheKey, out Like singleLike))
                {
                    var updatedLike = await _likeDal.GetAsync(a => a.Id == id.Value);
                    if (updatedLike != null)
                    {
                        _memoryCache.Set(likeCacheKey, updatedLike, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
