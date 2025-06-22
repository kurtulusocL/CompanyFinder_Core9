using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IToDoDal : IEntityRepository<ToDo>
    {
        ToDo HitRead(int? id);
        Task<bool> SetFinishedAsync(int id);
        Task<bool> SetNotFinishedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
