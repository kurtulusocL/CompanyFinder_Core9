using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IUserSessionService
    {
        Task<IEnumerable<UserSession>> GetAllIncludingAsync();
        Task<IEnumerable<UserSession>> GetAllIncludingCurrentlyLoginAsync();
        Task<IEnumerable<UserSession>> GetAllIncludingByLoginDateByUserIdAsync(string userId);
        Task<IEnumerable<UserSession>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<UserSession>> GetAllIncludingUserIdForAdminAsync(string userId);
        Task<IEnumerable<UserSession>> GetAllIncludingForAdminAsync();
        Task<UserSession> GetByIdAsync(int? id);
        Task<bool> DeleteAsync(UserSession entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<UserSession> GetAllUserSessionForAdminHome();
    }
}
