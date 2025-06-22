using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.BlogCache
{
    public class BlogCacheManager : IBlogCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IBlogDal _blogDal;
        public BlogCacheManager(IMemoryCache memoryCache, IBlogDal blogDal)
        {
            _memoryCache = memoryCache;
            _blogDal = blogDal;
        }

        public async Task BlogUpdateCache(int? categoryId, int? companyId, string userId, int? id)
        {
            if (_memoryCache.TryGetValue(BlogCacheKeys.GetAllBlogs, out List<Blog> allBlogs))
            {
                var updatedBlogs = await _blogDal.GetAllAsync();
                _memoryCache.Set(BlogCacheKeys.GetAllBlogs, updatedBlogs, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(BlogCacheKeys.GetAllBlogsForAdmin, out List<Blog> adminBlogs))
            {
                var updatedBlogs = await _blogDal.GetAllAsync();
                _memoryCache.Set(BlogCacheKeys.GetAllBlogsForAdmin, updatedBlogs, TimeSpan.FromMinutes(25));
            }

            if (companyId.HasValue)
            {
                string companyCacheKey = BlogCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyCacheKey, out List<Blog> companyBlogs))
                {
                    var updatedCompanyBlogs = await _blogDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyCacheKey, updatedCompanyBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (categoryId.HasValue)
            {
                string categoryCacheKey = BlogCacheKeys.GetAllIncludingByCategoryIdAsync(categoryId.Value);
                if (_memoryCache.TryGetValue(categoryCacheKey, out List<Blog> companyBlogs))
                {
                    var updatedCategoryBlogs = await _blogDal.GetAllAsync(i => i.BlogCategoryId == categoryId.Value);
                    _memoryCache.Set(categoryCacheKey, updatedCategoryBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = BlogCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Blog> userBlogs))
                {
                    var updatedUserBlogs = await _blogDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = BlogCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Blog> userAdminBlogs))
                {
                    var updatedUserAdminBlogs = await _blogDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = BlogCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Blog singleBlog))
                {
                    var updatedBlog = await _blogDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
