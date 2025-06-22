using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IAdCompanyDal : IEntityRepository<AdCompany>
    {
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}
