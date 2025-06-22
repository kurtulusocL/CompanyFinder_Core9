using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IQuestionAnswerService
    {
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingAsync();
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingByQuestionIdAsync(int? questionId);
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdmin();
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdminByQuestionIdAsync(int? questionId);
        Task<QuestionAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string? title, string answer, int? questionId, string userId);
        Task<bool> DeleteAsync(QuestionAnswer entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int QuestionAnswerCounter();
        IEnumerable<QuestionAnswer> GetAllIncludingQuestionAnswerByQuestionId(int? questionId);
        QuestionAnswer GetQuestionAnswerById(int? id);
    }
}
