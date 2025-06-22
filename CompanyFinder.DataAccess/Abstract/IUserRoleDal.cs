using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IUserRoleDal : IEntityRepository<UserRole>
    {
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetNotDeletedAsync(string id);
    }
}
