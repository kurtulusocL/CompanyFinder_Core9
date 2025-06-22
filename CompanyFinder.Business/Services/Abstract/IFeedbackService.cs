using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<IEnumerable<Feedback>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<Feedback>> GetAllForAdminAsync();
        Task<Feedback> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string subject, string desc, string userId);
        Task<bool> DeleteAsync(Feedback entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
