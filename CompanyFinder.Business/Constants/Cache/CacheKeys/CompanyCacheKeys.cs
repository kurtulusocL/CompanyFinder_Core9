namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class CompanyCacheKeys
    {
        public const string GetAllCompanies = "GetAllCompanies";
        public const string GetAllCommentables = "GetAllCommentableCompanies";
        public const string GetAllNotCommentables = "GetAllNoCommentableCompanies";
        public const string GetAllByFoundationDate = "GetAllCompaniesByFoundationDate";
        public const string GetAllForAdmin = "GetAllCompaniesForAdmin";

        public static string GetAllIncludingByCategoryIdAsync(int categoryId) => $"GetAllCompaniesByCategoryId_{categoryId}";
        public static string GetAllIncludingByCityIdAsync(int? cityId) => $"GetAllCompaniesByCityId_{cityId}";
        public static string GetAllIncludingByCountryIdAsync(int countryId) => $"GetAllCompaniesByCountryId_{countryId}";
        public static string GetAllIncludingBySubcategoryIdAsync(int? subcategoryId) => $"GetAllCompaniesBySubcategoryId_{subcategoryId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllCompaniesByUserId_{userId}";
        public static string GetAllIncludingByUserIdForAdminAsync(string userId) => $"GetAllCompaniesByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id) => $"GetCompanyById_{id}";
    }
}
