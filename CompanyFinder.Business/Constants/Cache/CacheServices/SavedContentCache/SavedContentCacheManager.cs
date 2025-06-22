using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.SavedContentCache
{
    public class SavedContentCacheManager : ISavedContentCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly ISavedContentDal _savedContentDal;
        public SavedContentCacheManager(IMemoryCache memoryCache, ISavedContentDal savedContentDal)
        {
            _memoryCache = memoryCache;
            _savedContentDal = savedContentDal;
        }

        public async Task SavedContentCacheClear(int? companyId, int? productId, int? blogId, string userId, int? id)
        {
            if (_memoryCache.TryGetValue(SavedContentCacheKeys.GetAllSavedContents, out List<SavedContent> allSavedContents))
            {
                var updatedSavedContents = await _savedContentDal.GetAllAsync(i=>i.IsSaved);
                _memoryCache.Set(SavedContentCacheKeys.GetAllSavedContents, updatedSavedContents, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(SavedContentCacheKeys.GetAllNotSavedContents, out List<SavedContent> allNotSavedContents))
            {
                var updatedNotSavedContents = await _savedContentDal.GetAllAsync(i=>!i.IsSaved);
                _memoryCache.Set(SavedContentCacheKeys.GetAllNotSavedContents, updatedNotSavedContents, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(SavedContentCacheKeys.GetAllSavedContentsForAdmin, out List<SavedContent> allSavedContentsForAdmin))
            {
                var updatedSavedContentsForAdmin = await _savedContentDal.GetAllAsync(i => i.IsSaved);
                _memoryCache.Set(SavedContentCacheKeys.GetAllSavedContentsForAdmin, updatedSavedContentsForAdmin, TimeSpan.FromMinutes(25));
            }

            if (companyId.HasValue)
            {
                string companySavedContentCacheKey = SavedContentCacheKeys.GetAllIncludingSavedByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companySavedContentCacheKey, out List<SavedContent> companySavedContents))
                {
                    var updatedCompanySavedContents = await _savedContentDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companySavedContentCacheKey, updatedCompanySavedContents, TimeSpan.FromMinutes(25));
                }
            }

            if (productId.HasValue)
            {
                string productSavedContentCacheKey = SavedContentCacheKeys.GetAllIncludingSavedByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productSavedContentCacheKey, out List<SavedContent> productSavedContents))
                {
                    var updatedProcuctSavedContents = await _savedContentDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productSavedContentCacheKey, updatedProcuctSavedContents, TimeSpan.FromMinutes(25));
                }
            }

            if (blogId.HasValue)
            {
                string blogSavedContentCacheKey = SavedContentCacheKeys.GetAllIncludingSavedByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogSavedContentCacheKey, out List<SavedContent> blogSavedContents))
                {
                    var updatedBlogSavedContents = await _savedContentDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogSavedContentCacheKey, updatedBlogSavedContents, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userSavedContentCacheKey = SavedContentCacheKeys.GetAllIncludingSavedByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userSavedContentCacheKey, out List<SavedContent> userSavedContents))
                {
                    var updatedUserSavedContents = await _savedContentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userSavedContentCacheKey, updatedUserSavedContents, TimeSpan.FromMinutes(25));
                }

                string userSavedContentForAdminCacheKey = SavedContentCacheKeys.GetAllIncludingSavedByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userSavedContentForAdminCacheKey, out List<SavedContent> userSavedContentsForAdmin))
                {
                    var updatedUserSavedContentsForAdmin = await _savedContentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userSavedContentForAdminCacheKey, updatedUserSavedContentsForAdmin, TimeSpan.FromMinutes(25));
                }

                string userNotSavedContentForAdminCacheKey = SavedContentCacheKeys.GetAllIncludingNotSavedByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userNotSavedContentForAdminCacheKey, out List<SavedContent> userNotSavedContentsForAdmin))
                {
                    var updatedUserNotSavedContentsForAdmin = await _savedContentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userNotSavedContentForAdminCacheKey, updatedUserNotSavedContentsForAdmin, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string savedContentCacheKey = SavedContentCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(savedContentCacheKey, out SavedContent singleSavedContent))
                {
                    var updatedSavedContent = await _savedContentDal.GetAsync(a => a.Id == id.Value);
                    if (updatedSavedContent != null)
                    {
                        _memoryCache.Set(savedContentCacheKey, updatedSavedContent, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
