namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class BlogCacheKeys
    {
        public const string GetAllBlogs = "GetAllBlogs";
        public const string GetAllBlogsForAdmin = "GetAllBlogsForAdmin";

        public static string GetAllIncludingByCategoryIdAsync(int categoryId)=> $"GetAllIncludeBlogByCategoryId_{categoryId}";
        public static string GetAllIncludingByCompanyIdAsync(int? companyId)=> $"GetAllIncludeBlogByCompanyId_{companyId}";
        public static string GetAllIncludingByUserIdAsync(string userId)=> $"GetAllIncludeBlogByUserId_{userId}";
        public static string GetAllIncludingForAdminByUserIdAsync(string userId)=> $"GetAllIncludeBlogForAdminByUserId_{userId}";
        public static string GetByIdAsync(int? id)=> $"GetBlogById_{id}";
    }
}
