using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IReportDal : IEntityRepository<Report>
    {
        Report HitRead(int? id);
        int ReportCounter();
        Task<bool> SetFixedAsync(int id);
        Task<bool> SetNotFixedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}