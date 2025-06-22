namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class QuestionAnswerCacheKeys
    {
        public const string GetAllQuestionAnswers = "GetAllQuestionAnswers";
        public const string GetAllQuestionAnswerForAdmin = "GetAllQuestionAnswersForAdmin";

        public static string GetAllIncludingByQuestionIdAsync(int? questionId) => $"GetAllIncludingQuestionAnswerByQuestionId_{questionId}";
        public static string GetAllIncludingForAdminByQuestionIdAsync(int? questionId) => $"GetAllIncludingQuestionAnswerForAdminByQuestionId_{questionId}";
        public static string GetAllIncludingByUserIdAsync(string userId) => $"GetAllIncludingQuestionAnswerByUserId_{userId}";
        public static string GetAllIncludingForAdminByUserIdAsync(string userId) => $"GetAllIncludingQuestionAnswerForAdminByUserId_{userId}";
        public static string GetByIdAsync(int? id) => $"GetQuestionAnswerById_{id}";
    }
}
