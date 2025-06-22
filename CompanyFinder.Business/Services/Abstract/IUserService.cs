using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllIncludingAsync();
        Task<IEnumerable<User>> GetAllIncludingAdminUserAsync();
        Task<IEnumerable<User>> GetAllIncludingCompanyUserAsync();
        Task<IEnumerable<User>> GetAllIncludingSuspendedAdminAsync();
        Task<IEnumerable<User>> GetAllIncludingSuspendedCompanyAsync();
        Task<IEnumerable<User>> GetAllIncludingDeletedAdminAsync();
        Task<IEnumerable<User>> GetAllIncludingDeletedCompanyAsync();
        Task<IEnumerable<User>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<User>> GetAllAdminForAddTask();
        Task<User> GetByIdAsync(string id);
        Task<bool> DeleteAsync(User entity, string id);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
        User GetUserForAdminHomeById(string id);
        User GetUserCompanyHeaderByUserId(string userId);
        Task<User> GetCompanyUserForProfileByUserIdAsync(string id);
        int UserCounter();
    }
}
