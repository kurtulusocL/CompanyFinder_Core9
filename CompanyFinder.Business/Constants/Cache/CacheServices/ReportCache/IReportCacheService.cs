using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.ReportCache
{
    public interface IReportCacheService
    {
        Task UpdateClearCache();
        Task BlogCacheClear(int? blogId, int categoryId, string userId, int? id);
        Task CommentCacheClear(int? commentAnswerId, int categoryId, string userId, int? id);
        Task CommentAnswerCacheClear(int? commentId, int categoryId, string userId, int? id);
        Task CompanyCacheClear(int? companyId, int categoryId, string userId, int? id);
        Task ProductCacheClear(int? productId, int categoryId, string userId, int? id);
    }
}
