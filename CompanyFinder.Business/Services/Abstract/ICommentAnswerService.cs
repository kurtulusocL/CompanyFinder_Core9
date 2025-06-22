using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICommentAnswerService
    {
        Task<IEnumerable<CommentAnswer>> GetAllIncludingAsync();
        Task<IEnumerable<CommentAnswer>> GetAllIncludingByCommentIdAsync(int? commentId);
        Task<IEnumerable<CommentAnswer>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdmin();
        Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdminByCommentIdAsync(int? commentId);
        Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<CommentAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string? title, string text, int? commentId, string userId);
        Task<bool> DeleteAsync(CommentAnswer entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int CommentAnswerCounter();
        IEnumerable<CommentAnswer> GetAllSiteMap();
        IEnumerable<CommentAnswer> GetAllIncludingCommentAnswerForCompanyByCommentId(int? commentId);
        IEnumerable<CommentAnswer> GetAllIncludingCommentAnswersForExplorerByCommentId(int? commentId);
    }
}
