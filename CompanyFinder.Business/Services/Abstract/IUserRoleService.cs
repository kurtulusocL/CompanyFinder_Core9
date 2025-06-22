using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<IEnumerable<UserRole>> GetAllForAdmin();
        Task<UserRole> GetByIdAsync(string id);
        Task<bool> CreateAsync(UserRole entity);
        Task<bool> UpdateAsync(UserRole entity);
        Task<bool> DeleteAsync(UserRole entity, string id);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
