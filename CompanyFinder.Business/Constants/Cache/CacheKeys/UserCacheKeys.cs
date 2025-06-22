using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class UserCacheKeys
    {
        public const string GetAllIncludingAdminUser = "GetAllIncludingAdminUser";
        public const string GetAllIncluding = "GetAllIncluding";
        public const string GetAllIncludingCompanyUser = "GetAllIncludingCompanyUser";
        public const string GetAllIncludingDeletedAdmin = "GetAllIncludingDeletedAdmin";
        public const string GetAllIncludingDeletedCompany = "GetAllIncludingDeletedCompany";
        public const string GetAllIncludingForAdmin = "GetAllIncludingForAdmin";
        public const string GetAllIncludingSuspendedAdmin = "GetAllIncludingSuspendedAdmin";
        public const string GetAllIncludingSuspendedCompany = "GetAllIncludingSuspendedCompany";
        public static string GetByIdAsync(string id) => $"GetUserById_{id}";
    }
}
