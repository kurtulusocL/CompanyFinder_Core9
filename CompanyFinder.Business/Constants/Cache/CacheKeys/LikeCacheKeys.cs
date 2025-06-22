namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class LikeCacheKeys
    {
        public const string GetAllLikes = "GetAllLikes";
        public const string GetAllLikesValueAsc = "GetAllLikesByValueAsc";
        public const string GetAllLikesValueDesc = "GetAllLikesByValueDesc";
        public const string GetAllLikesForAdmin = "GetAllLikesForAdmin";

        public static string GetAllIncludingByBlogIdAsync(int? blogId) => $"GetAllLikesByBlogId_{blogId}";
        public static string GetAllIncludingByBlogIdForAdminAsync(int? blogId) => $"GetAllLikesByBlogIdForAdmin_{blogId}";
        public static string GetAllIncludingByCompanyIdAsync(int? companyId) => $"GetAllLikesByCompanyId_{companyId}";
        public static string GetAllIncludingByCompanyIdForAdminAsync(int? companyId) => $"GetAllLikesByCompanyIdForAdmin_{companyId}";
        public static string GetAllIncludingByProductIdAsync(int? productId) => $"GetAllLikesByProductId_{productId}";
        public static string GetAllIncludingByProductIdForAdminAsync(int? productId) => $"GetAllLikesByProductIdForAdmin_{productId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllLikesByUserId_{userId}";
        public static string GetAllIncludingByUserIdForAdminAsync(string userId) => $"GetAllLikesByUserId_{userId}";
        public static string GetByIdAsync(int? id) => $"GetLikesById_{id}";
    }
}
