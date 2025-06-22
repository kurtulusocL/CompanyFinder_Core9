using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class SavedContentCacheKeys
    {
        public const string GetAllSavedContents = "GetAllSavedContents";
        public const string GetAllNotSavedContents = "GetAllNotSavedContents";
        public const string GetAllSavedContentsForAdmin = "GetAllSavedContentsForAdmin";

        public static string GetAllIncludingNotSavedByUserIdForAdminAsync(string userId) => $"GetAllIncludingNotSavedContentByUserIdForAdmin_{userId}";
        public static string GetAllIncludingSavedByUserIdForAdminAsync(string userId) => $"GetAllIncludingSavedContentByUserIdForAdmin_{userId}";
        public static string GetAllIncludingSavedByUserIdAsync(string userId) => $"GetAllIncludingNotSavedContentByUserId_{userId}";
        public static string GetAllIncludingSavedByBlogIdAsync(int? blogId) => $"GetAllIncludingSavedContentByBlogId_{blogId}";
        public static string GetAllIncludingSavedByCompanyIdAsync(int? companyId) => $"GetAllIncludingNotSavedContentByCompanyId_{companyId}";
        public static string GetAllIncludingSavedByProductIdAsync(int? productId) => $"GetAllIncludingNotSavedContentByProductId_{productId}";
        public static string GetByIdAsync(int? id) => $"GetSavedContentById_{id}";
    }
}
