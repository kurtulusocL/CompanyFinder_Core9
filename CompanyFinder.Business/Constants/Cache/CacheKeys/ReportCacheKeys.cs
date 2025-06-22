namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class ReportCacheKeys
    {
        public const string GetAllReports = "GetAllReports";
        public const string GetAllFixedReports = "GetAllFixedReports";
        public const string GetAllNotFixedReports = "GetAllNotFixedReports";       
        public const string GetAllReportsForAdmin = "GetAllReportsForAdmin";

        public static string GetAllIncludingReportByBlogIdAsync(int? blogId) => $"GetAllIncludingReportByBlogId_{blogId}";
        public static string GetAllIncludingReportByCategoryIdAsync(int categoryId) => $"GetAllIncludingReportByCategoryId_{categoryId}";
        public static string GetAllIncludingReportByCommentAnswerIdAsync(int? commentAnswerId) => $"GetAllIncludingReportByCommentAnswerId_{commentAnswerId}";
        public static string GetAllIncludingReportByCommentIdAsync(int? commentId) => $"GetAllIncludingReportByCommentId_{commentId}";
        public static string GetAllIncludingReportByCompanyIdAsync(int? companyId) => $"GetAllIncludingReportByCompanyId_{companyId}";
        public static string GetAllIncludingReportByProductIdAsync(int? productId) => $"GetAllIncludingReportByProductId_{productId}";
        public static string GetAllIncludingReportByUserIdAsync(string userId) => $"GetAllIncludingReportByUserId_{userId}";
        public static string GetAllIncludingReportForAdminByUserIdAsync(string userId) => $"GetAllIncludingReportByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id) => $"GetReportById_{id}";
    }
}
