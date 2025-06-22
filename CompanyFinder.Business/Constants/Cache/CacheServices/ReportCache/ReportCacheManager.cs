using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.ReportCache
{
    public class ReportCacheManager : IReportCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IReportDal _reportDal;
        public ReportCacheManager(IMemoryCache memoryCache, IReportDal reportDal)
        {
            _memoryCache = memoryCache;
            _reportDal = reportDal;
        }

        public async Task BlogCacheClear(int? blogId, int categoryId, string userId, int? id)
        {
            if (blogId.HasValue)
            {
                string blogReportCacheKey = ReportCacheKeys.GetAllIncludingReportByBlogIdAsync(blogId.Value);
                if (_memoryCache.TryGetValue(blogReportCacheKey, out List<Report> companyBlogReports))
                {
                    var updatedBlogReportCaches = await _reportDal.GetAllAsync(i => i.BlogId == blogId.Value);
                    _memoryCache.Set(blogReportCacheKey, updatedBlogReportCaches, TimeSpan.FromMinutes(25));
                }
            }

            string reportCategoryCacheKey = ReportCacheKeys.GetAllIncludingReportByCategoryIdAsync(categoryId);
            if (_memoryCache.TryGetValue(reportCategoryCacheKey, out List<Report> reportCategories))
            {
                var updatedReportCategoryCaches = await _reportDal.GetAllAsync(i => i.ReportCategoryId == categoryId);
                _memoryCache.Set(reportCategoryCacheKey, updatedReportCategoryCaches, TimeSpan.FromMinutes(25));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ReportCacheKeys.GetAllIncludingReportByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Report> userBlogs))
                {
                    var updatedUserBlogs = await _reportDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = ReportCacheKeys.GetAllIncludingReportForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Report> userAdminBlogs))
                {
                    var updatedUserAdminBlogs = await _reportDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = ReportCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Report singleReport))
                {
                    var updatedBlog = await _reportDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task CommentAnswerCacheClear(int? commentAnswerId, int categoryId, string userId, int? id)
        {
            if (commentAnswerId.HasValue)
            {
                string commentAnswerReportCacheKey = ReportCacheKeys.GetAllIncludingReportByCommentAnswerIdAsync(commentAnswerId.Value);
                if (_memoryCache.TryGetValue(commentAnswerReportCacheKey, out List<Report> commentAnswerReports))
                {
                    var updatedCommentAnswerReportCaches = await _reportDal.GetAllAsync(i => i.CommentAnswerId == commentAnswerId.Value);
                    _memoryCache.Set(commentAnswerReportCacheKey, updatedCommentAnswerReportCaches, TimeSpan.FromMinutes(25));
                }
            }

            string reportCategoryCacheKey = ReportCacheKeys.GetAllIncludingReportByCategoryIdAsync(categoryId);
            if (_memoryCache.TryGetValue(reportCategoryCacheKey, out List<Report> reportCategories))
            {
                var updatedReportCategoryCaches = await _reportDal.GetAllAsync(i => i.ReportCategoryId == categoryId);
                _memoryCache.Set(reportCategoryCacheKey, updatedReportCategoryCaches, TimeSpan.FromMinutes(25));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ReportCacheKeys.GetAllIncludingReportByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Report> userBlogs))
                {
                    var updatedUserBlogs = await _reportDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = ReportCacheKeys.GetAllIncludingReportForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Report> userAdminBlogs))
                {
                    var updatedUserAdminBlogs = await _reportDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = ReportCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Report singleReport))
                {
                    var updatedBlog = await _reportDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task CommentCacheClear(int? commentId, int categoryId, string userId, int? id)
        {
            if (commentId.HasValue)
            {
                string commentReportCacheKey = ReportCacheKeys.GetAllIncludingReportByCommentIdAsync(commentId.Value);
                if (_memoryCache.TryGetValue(commentReportCacheKey, out List<Report> commentReports))
                {
                    var updatedAnswerReportCaches = await _reportDal.GetAllAsync(i => i.CommentId == commentId.Value);
                    _memoryCache.Set(commentReportCacheKey, updatedAnswerReportCaches, TimeSpan.FromMinutes(25));
                }
            }

            string reportCategoryCacheKey = ReportCacheKeys.GetAllIncludingReportByCategoryIdAsync(categoryId);
            if (_memoryCache.TryGetValue(reportCategoryCacheKey, out List<Report> reportCategories))
            {
                var updatedReportCategoryCaches = await _reportDal.GetAllAsync(i => i.ReportCategoryId == categoryId);
                _memoryCache.Set(reportCategoryCacheKey, updatedReportCategoryCaches, TimeSpan.FromMinutes(25));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ReportCacheKeys.GetAllIncludingReportByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Report> userBlogs))
                {
                    var updatedUserBlogs = await _reportDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = ReportCacheKeys.GetAllIncludingReportForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Report> userAdminBlogs))
                {
                    var updatedUserAdminBlogs = await _reportDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = ReportCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Report singleReport))
                {
                    var updatedBlog = await _reportDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task CompanyCacheClear(int? companyId, int categoryId, string userId, int? id)
        {
            if (companyId.HasValue)
            {
                string companyReportCacheKey = ReportCacheKeys.GetAllIncludingReportByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(companyReportCacheKey, out List<Report> companyReports))
                {
                    var updatedCompanyReportCaches = await _reportDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(companyReportCacheKey, updatedCompanyReportCaches, TimeSpan.FromMinutes(25));
                }
            }

            string reportCategoryCacheKey = ReportCacheKeys.GetAllIncludingReportByCategoryIdAsync(categoryId);
            if (_memoryCache.TryGetValue(reportCategoryCacheKey, out List<Report> reportCategories))
            {
                var updatedReportCategoryCaches = await _reportDal.GetAllAsync(i => i.ReportCategoryId == categoryId);
                _memoryCache.Set(reportCategoryCacheKey, updatedReportCategoryCaches, TimeSpan.FromMinutes(25));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ReportCacheKeys.GetAllIncludingReportByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Report> userBlogs))
                {
                    var updatedUserBlogs = await _reportDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = ReportCacheKeys.GetAllIncludingReportForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Report> userAdminReports))
                {
                    var updatedUserAdminBlogs = await _reportDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = ReportCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Report singleReport))
                {
                    var updatedBlog = await _reportDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ProductCacheClear(int? productId, int categoryId, string userId, int? id)
        {
            if (productId.HasValue)
            {
                string productReportCacheKey = ReportCacheKeys.GetAllIncludingReportByProductIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(productReportCacheKey, out List<Report> productReports))
                {
                    var updatedProductReportCaches = await _reportDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(productReportCacheKey, updatedProductReportCaches, TimeSpan.FromMinutes(25));
                }
            }

            string reportCategoryCacheKey = ReportCacheKeys.GetAllIncludingReportByCategoryIdAsync(categoryId);
            if (_memoryCache.TryGetValue(reportCategoryCacheKey, out List<Report> reportCategories))
            {
                var updatedReportCategoryCaches = await _reportDal.GetAllAsync(i => i.ReportCategoryId == categoryId);
                _memoryCache.Set(reportCategoryCacheKey, updatedReportCategoryCaches, TimeSpan.FromMinutes(25));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ReportCacheKeys.GetAllIncludingReportByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Report> userBlogs))
                {
                    var updatedUserBlogs = await _reportDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserBlogs, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = ReportCacheKeys.GetAllIncludingReportForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Report> userAdminReports))
                {
                    var updatedUserAdminBlogs = await _reportDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminBlogs, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string blogCacheKey = ReportCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(blogCacheKey, out Report singleReport))
                {
                    var updatedBlog = await _reportDal.GetAsync(a => a.Id == id.Value);
                    if (updatedBlog != null)
                    {
                        _memoryCache.Set(blogCacheKey, updatedBlog, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task UpdateClearCache()
        {
            if (_memoryCache.TryGetValue(ReportCacheKeys.GetAllReports, out List<Report> allReports))
            {
                var updatedReports = await _reportDal.GetAllAsync();
                _memoryCache.Set(ReportCacheKeys.GetAllReports, updatedReports, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ReportCacheKeys.GetAllFixedReports, out List<Report> allFixedReports))
            {
                var updatedFixedReports = await _reportDal.GetAllAsync(i => i.IsFixed);
                _memoryCache.Set(ReportCacheKeys.GetAllFixedReports, updatedFixedReports, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ReportCacheKeys.GetAllNotFixedReports, out List<Report> allNotFixedReports))
            {
                var updatedNotFixedReports = await _reportDal.GetAllAsync(i => !i.IsFixed);
                _memoryCache.Set(ReportCacheKeys.GetAllNotFixedReports, updatedNotFixedReports, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ReportCacheKeys.GetAllReportsForAdmin, out List<Report> allAdminReports))
            {
                var updatedAdminReports = await _reportDal.GetAllAsync();
                _memoryCache.Set(ReportCacheKeys.GetAllReportsForAdmin, updatedAdminReports, TimeSpan.FromMinutes(25));
            }
        }
    }
}
