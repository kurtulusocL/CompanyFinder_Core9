namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public class CommentAnswerCacheKeys
    {
        public const string AllCommentAnswers = "GetAllCommentAnswers";
        public const string AllCommentAnswerForAdmin = "GetAllCommentAnswersForAdmin";

        public static string GetAllIncludingByCommentIdAsync(int? commentId)=> $"GetAllIncludingCommentAnswersByCommentId_{commentId}";
        public static string GetAllIncludingForAdminByCommentIdAsync(int? commentId)=> $"GetAllIncludingCommentAnswersByCommentIdForAdmin_{commentId}";
        public static string GetAllIncludingByUserIdAsync(string userId)=> $"GetAllIncludingCommentAnswersByUserId_{userId}";
        public static string GetAllIncludingForAdminByUserIdAsync(string userId)=> $"GetAllIncludingCommentAnswersByUserIdForAdmin_{userId}";
        public static string GetByIdAsync(int? id)=> $"GetCommentAnswerById_{id}";
    }
}
