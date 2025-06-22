using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IBlogDal : IEntityRepository<Blog>
    {
        int BlogCounter();
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}
