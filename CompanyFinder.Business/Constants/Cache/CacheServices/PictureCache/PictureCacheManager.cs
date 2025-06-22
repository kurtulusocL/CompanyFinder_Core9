using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.PictureCache
{
    public class PictureCacheManager : IPictureCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IPictureDal _pictureDal;
        public PictureCacheManager(IMemoryCache memoryCache, IPictureDal pictureDal)
        {
            _pictureDal = pictureDal;
            _memoryCache = memoryCache;
        }
        public async Task ClarAndUpdateCache()
        {
            if (_memoryCache.TryGetValue(PictureCacheKeys.GetAllPictures, out List<Picture> allPictures))
            {
                var updatedPictures = await _pictureDal.GetAllAsync();
                _memoryCache.Set(PictureCacheKeys.GetAllPictures, updatedPictures, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(PictureCacheKeys.GetAllPicturesForAdmin, out List<Picture> allPicturesForAdmin))
            {
                var updatedPicturesForAdmin = await _pictureDal.GetAllAsync();
                _memoryCache.Set(PictureCacheKeys.GetAllPicturesForAdmin, updatedPicturesForAdmin, TimeSpan.FromMinutes(25));
            }
        }

        public async Task ClearBlogCache(int? blogId, int? id)
        {
            if (blogId.HasValue)
            {
                string blogPictureCacheKey = PictureCacheKeys.GetAllIncludingByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogPictureCacheKey, out List<Picture> blogPictures))
                {
                    var updatedPicturePictureCacheKeys = await _pictureDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogPictureCacheKey, updatedPicturePictureCacheKeys, TimeSpan.FromMinutes(25));
                }

                string blogPictureAdminCacheKey = PictureCacheKeys.GetAllIncludingByBlogIdForAdminAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogPictureAdminCacheKey, out List<Picture> adminBlogPictures))
                {
                    var updatedAdminBlogPictureCacheKeys = await _pictureDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogPictureAdminCacheKey, updatedAdminBlogPictureCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string pictureCacheKey = PictureCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(pictureCacheKey, out Picture singlePicture))
                {
                    var updatedPicture = await _pictureDal.GetAsync(a => a.Id == id.Value);
                    if (updatedPicture != null)
                    {
                        _memoryCache.Set(pictureCacheKey, updatedPicture, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ClearCompanyCache(int? companyId, int? id)
        {
            if (companyId.HasValue)
            {
                string companyPictureCacheKey = PictureCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyPictureCacheKey, out List<Picture> companyPictures))
                {
                    var updatedCompanyPictureCacheKeys = await _pictureDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyPictureCacheKey, updatedCompanyPictureCacheKeys, TimeSpan.FromMinutes(25));
                }

                string companyPictureAdminCacheKey = PictureCacheKeys.GetAllIncludingByCompanyIdForAdminAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyPictureAdminCacheKey, out List<Picture> adminCompanyPictures))
                {
                    var updatedAdminCompanyPictureCacheKeys = await _pictureDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyPictureAdminCacheKey, updatedAdminCompanyPictureCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string pictureCacheKey = PictureCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(pictureCacheKey, out Picture singlePicture))
                {
                    var updatedPicture = await _pictureDal.GetAsync(a => a.Id == id.Value);
                    if (updatedPicture != null)
                    {
                        _memoryCache.Set(pictureCacheKey, updatedPicture, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ClearProductCache(int? productId, int? id)
        {
            if (productId.HasValue)
            {
                string productPictureCacheKey = PictureCacheKeys.GetAllIncludingByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productPictureCacheKey, out List<Picture> productPictures))
                {
                    var updatedProductPictureCacheKeys = await _pictureDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productPictureCacheKey, updatedProductPictureCacheKeys, TimeSpan.FromMinutes(25));
                }

                string productPictureAdminCacheKey = PictureCacheKeys.GetAllIncludingByProductIdForAdminAsync(productId.Value);
                if (_memoryCache.TryGetValue(productPictureAdminCacheKey, out List<Picture> adminProductPictures))
                {
                    var updatedAdminProductPictureCacheKeys = await _pictureDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productPictureAdminCacheKey, updatedAdminProductPictureCacheKeys, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string pictureCacheKey = PictureCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(pictureCacheKey, out Picture singlePicture))
                {
                    var updatedPicture = await _pictureDal.GetAsync(a => a.Id == id.Value);
                    if (updatedPicture != null)
                    {
                        _memoryCache.Set(pictureCacheKey, updatedPicture, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
