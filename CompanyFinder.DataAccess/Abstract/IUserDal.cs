using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        int UserCounter();
        Task<bool> SetNotDeletedAsync(string id);
        Task<bool> SetDeletedAsync(string id);
        Task<bool> SetActiveAsync(string id);
        Task<bool> SetDeActiveAsync(string id);
    }
}
