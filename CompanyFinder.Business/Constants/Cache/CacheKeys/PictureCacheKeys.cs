namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class PictureCacheKeys
    {
        public const string GetAllPictures = "GetAllPictures";
        public const string GetAllPicturesForAdmin = "GetAllPicturesForAdmin";

        public static string GetAllIncludingByBlogIdAsync(int? blogId) => $"GetAllIncludingPicturesByBlogId_{blogId}";
        public static string GetAllIncludingByBlogIdForAdminAsync(int? blogId) => $"GetAllIncludingPicturesByBlogIdForAdmin_{blogId}";
        public static string GetAllIncludingByCompanyIdAsync(int? companyId) => $"GetAllIncludingPicturesByCompanyId_{companyId}";
        public static string GetAllIncludingByCompanyIdForAdminAsync(int? companyId) => $"GetAllIncludingPicturesByCompanyIdForAdmin_{companyId}";
        public static string GetAllIncludingByProductIdAsync(int? productId) => $"GetAllIncludingPicturesByProductId_{productId}";
        public static string GetAllIncludingByProductIdForAdminAsync(int? productId) => $"GetAllIncludingPicturesByProductIdForAdmin_{productId}";
        public static string GetByIdAsync(int? id) => $"GetPicturesById_{id}";
    }
}
