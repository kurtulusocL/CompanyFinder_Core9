using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.HitCache
{
    public class HitCacheManager : IHitCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IHitDal _hitDal;
        public HitCacheManager(IMemoryCache memoryCache, IHitDal hitDal)
        {
            _memoryCache = memoryCache;
            _hitDal = hitDal;
        }
        public void AdHitCacheUpdate(int? adId, string? userId, int? id)
        {
            if (adId.HasValue)
            {
                string adHitCacheKey = HitCacheKeys.GetAllIncludingByAdIdAsync(adId.Value);
                if (_memoryCache.TryGetValue(adHitCacheKey, out List<Hit> adHits))
                {
                    var updatedAdHits = _hitDal.GetAll(i => i.AdId == adId.Value);
                    _memoryCache.Set(adHitCacheKey, updatedAdHits, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = HitCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Hit> userBlogs))
                {
                    var updatedUserHits = _hitDal.GetAll(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserHits, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = HitCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Hit> userAdminBlogs))
                {
                    var updatedUserAdminHits = _hitDal.GetAll(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminHits, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string hitCacheKey = HitCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(hitCacheKey, out Hit singleHit))
                {
                    var updatedHit = _hitDal.Get(a => a.Id == id.Value);
                    if (updatedHit != null)
                    {
                        _memoryCache.Set(hitCacheKey, updatedHit, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public void BlogHitCacheUpdate(int? blogId, string? userId, int? id)
        {
            if (blogId.HasValue)
            {
                string blogHitCacheKey = HitCacheKeys.GetAllIncludingByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogHitCacheKey, out List<Hit> blogHits))
                {
                    var updatedBlogHits = _hitDal.GetAll(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogHitCacheKey, updatedBlogHits, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = HitCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Hit> userBlogs))
                {
                    var updatedUserHits = _hitDal.GetAll(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserHits, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = HitCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Hit> userAdminBlogs))
                {
                    var updatedUserAdminHits = _hitDal.GetAll(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminHits, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string hitCacheKey = HitCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(hitCacheKey, out Hit singleHit))
                {
                    var updatedHit = _hitDal.Get(a => a.Id == id.Value);
                    if (updatedHit != null)
                    {
                        _memoryCache.Set(hitCacheKey, updatedHit, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public void ClearUpdateHitCaches()
        {
            if (_memoryCache.TryGetValue(HitCacheKeys.GetAllHits, out List<Blog> allHits))
            {
                var updatedHits = _hitDal.GetAll();
                _memoryCache.Set(HitCacheKeys.GetAllHits, updatedHits, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(HitCacheKeys.GetAllHitByValue, out List<Blog> allHitsByValue))
            {
                var updatedHitsValue = _hitDal.GetAll();
                _memoryCache.Set(HitCacheKeys.GetAllHitByValue, updatedHitsValue, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(HitCacheKeys.GetAllHitsByAdmin, out List<Blog> allHitsForAdmin))
            {
                var updatedHitsForAdmin = _hitDal.GetAll();
                _memoryCache.Set(HitCacheKeys.GetAllHitsByAdmin, updatedHitsForAdmin, TimeSpan.FromMinutes(25));
            }
        }

        public void CompanyHitCacheUpdate(int? companyId, string? userId, int? id)
        {
            if (companyId.HasValue)
            {
                string companyHitCacheKey = HitCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyHitCacheKey, out List<Hit> companyHits))
                {
                    var updatedCompanyHits = _hitDal.GetAll(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyHitCacheKey, updatedCompanyHits, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = HitCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Hit> companyHits))
                {
                    var updatedUserHits = _hitDal.GetAll(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserHits, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = HitCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Hit> userAdminBlogs))
                {
                    var updatedUserAdminHits = _hitDal.GetAll(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminHits, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string hitCacheKey = HitCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(hitCacheKey, out Hit singleHit))
                {
                    var updatedHit = _hitDal.Get(a => a.Id == id.Value);
                    if (updatedHit != null)
                    {
                        _memoryCache.Set(hitCacheKey, updatedHit, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public void ProductHitCacheUpdate(int? productId, string? userId, int? id)
        {
            if (productId.HasValue)
            {
                string productCacheKey = HitCacheKeys.GetAllIncludingByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productCacheKey, out List<Hit> productHits))
                {
                    var updatedProductHits = _hitDal.GetAll(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productCacheKey, updatedProductHits, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = HitCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Hit> userBlogs))
                {
                    var updatedUserHits = _hitDal.GetAll(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserHits, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = HitCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Hit> userAdminBlogs))
                {
                    var updatedUserAdminHits = _hitDal.GetAll(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminHits, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string hitCacheKey = HitCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(hitCacheKey, out Hit singleHit))
                {
                    var updatedHit = _hitDal.Get(a => a.Id == id.Value);
                    if (updatedHit != null)
                    {
                        _memoryCache.Set(hitCacheKey, updatedHit, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
