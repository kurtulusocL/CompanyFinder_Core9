using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllIncludingAsync();
        Task<IEnumerable<Question>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Question>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Question>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Question>> GetAllIncludingForAdmin();
        Task<IEnumerable<Question>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<Question> GetByIdAsync(int? id);
        Question HitRead(int? id);
        Task<bool> CreateCompanyQuestionAsync(string text, int? companyId, string userId);
        Task<bool> CreateProductQuestionAsync(string text, int? productId, string userId);
        Task<bool> DeleteAsync(Question entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int QuestionCounter();
        IEnumerable<Question> GetAllIncludingProductQuestionByProductId(int? productId);
        IEnumerable<Question> GetAllIncludingCompanyQuestionByCompanyId(int? companyId);
        Question GetQuestionById(int? id);
        Task<IEnumerable<Question>> GetAllIncludingCompanyQuestionsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Question>> GetAllIncludingProductQuestionsForCompanyUserByUserIdAsync(string userId);
    }
}
