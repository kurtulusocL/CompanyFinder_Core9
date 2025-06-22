namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class ProductCacheKeys
    {
        public const string GetAllProducts = "GetAllProducts";
        public const string GetAllQuestionableProducts = "GetAllQuestionableProducts";
        public const string GetAllNotQuestionableProducts = "GetAllNotQuestionableProducts";
        public const string GetAllAvailableProducts = "GetAllAvailableProducts";
        public const string GetAllNotAvailableProducts = "GetAllNotAvailableProducts";
        public const string GetAllCommentableProducts = "GetAllCommentableProducts";
        public const string GetAllNotCommentableProducts = "GetAllNotCommentableProducts";
        public const string GetAllProductsForAdmin = "GetAllProductsForAdmin";

        public static string GetAllIncludingByCompanyIdAsync(int companyId) => $"GetAllProductsByCompanyId_{companyId}";
        public static string GetAllIncludingByCompanyIdForAdminAsync(int companyId) => $"GetAllProductsByCompanyIdForAdmin_{companyId}";
        public static string GetAllIncludingByCategoryIdAsync(int categoryId) => $"GetAllProductsByCategoryId_{categoryId}";
        public static string GetAllIncludingBySubcategoryIdAsync(int? subcategoryId) => $"GetAllProductsBySubcategoryId_{subcategoryId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllProductsByUserId_{userId}";
        public static string GetAllIncludingByUserIdForAdminAsync(string userId) => $"GetAllProductsByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id) => $"GetProductById_{id}";
    }
}
