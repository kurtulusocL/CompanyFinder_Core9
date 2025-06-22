using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IAdDal : IEntityRepository<Ad>
    {
        Ad SimilarHitRead(int? id);
        int AdCounter();
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}
