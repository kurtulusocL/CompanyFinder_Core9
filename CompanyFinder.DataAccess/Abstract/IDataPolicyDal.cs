using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IDataPolicyDal : IEntityRepository<DataPolicy>
    {
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
