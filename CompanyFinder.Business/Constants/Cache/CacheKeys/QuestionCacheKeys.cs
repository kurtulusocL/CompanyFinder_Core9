using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class QuestionCacheKeys
    {
        public const string GetAllQuestions = "GetAllQuestions";
        public const string GetAllQuestionForAdmin = "GetAllQuestionsForAdmin";

        public static string GetAllIncludingByCompanyIdAsync(int? companyId) => $"GetAllIncludingQuestionsByCompanyId_{companyId}";
        public static string GetAllIncludingByProductIdAsync(int? productId) => $"GetAllIncludingQuestionsByProductId_{productId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllIncludingQuestionsByUserId_{userId}";
        public static string GetAllIncludingByUserIdForAdminAsync(string userId) => $"GetAllIncludingQuestionsByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id) => $"GetQuestinById_{id}";
    }
}
