namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class HitCacheKeys
    {
        public const string GetAllHits = "GetAllHits";
        public const string GetAllHitByValue = "GetAllHitsByValue";
        public const string GetAllHitsByAdmin = "GetAllHitsForAdmin";

        public static string GetAllIncludingByAdIdAsync(int? adId) => $"GetAllIncludingHitsByAdId_{adId}";
        public static string GetAllIncludingByBlogIdAsync(int? blogId) => $"GetAllIncludingHitsByBlogId_{blogId}";       
        public static string GetAllIncludingByCompanyIdAsync(int? companyId) => $"GetAllIncludingHitsByCompanyId_{companyId}";
        public static string GetAllIncludingByProductIdAsync(int? productId) => $"GetAllIncludingHitsByProductId_{productId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllIncludingHitsByUserId_{userId}";
        public static string GetAllIncludingForAdminByUserIdAsync(string userId) => $"GetAllIncludingHitsForAdminByUserId_{userId}";
        public static string GetByIdAsync(int? id) => $"GetHitById_{id}";
    }
}
