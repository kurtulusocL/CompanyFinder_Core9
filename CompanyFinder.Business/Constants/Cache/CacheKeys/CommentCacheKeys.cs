namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class CommentCacheKeys
    {
        public const string GetAllComments = "GetAllComments";
        public const string GetAllCommentsForAdmin = "GetAllCommentsForAdmin";

        public static string GetAllIncludingByBlogIdAsync(int? blogId)=> $"GetAllIncludingCommentByBlogId_{blogId}";
        public static string GetAllIncludingByBlogIdForAdminAsync(int? blogId)=> $"GetAllIncludingCommentByBlogIdForAdmin_{blogId}";
        public static string GetAllIncludingByCompanyIdAsync(int? companyId)=> $"GetAllIncludingCommentByCompanyId_{companyId}";
        public static string GetAllIncludingByCompanyIdForAdminAsync(int? companyId)=> $"GetAllIncludingCommentByCompanyIdForAdmin_{companyId}";
        public static string GetAllIncludingByProductIdAsync(int? productId)=> $"GetAllIncludingCommentByProductId_{productId}";
        public static string GetAllIncludingByProductIdForAdminAsync(int? productId)=> $"GetAllIncludingCommentByProductIdForAdmin_{productId}";
        public static string GetAllIncludingByUserIdAsync(string userId)=> $"GetAllIncludingCommentByUserId_{userId}";
        public static string GetAllIncludingByUserIdForAdminAsync(string userId)=> $"GetAllIncludingCommentByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id)=> $"GetCommentById_{id}";
    }
}
