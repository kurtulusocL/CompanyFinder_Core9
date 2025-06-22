using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IReportAnswerDal : IEntityRepository<ReportAnswer>
    {
        int ReportAnswerCounter();
        Task<bool> SetSatisfieldAsync(int id);
        Task<bool> SetNotSatisfieldAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
