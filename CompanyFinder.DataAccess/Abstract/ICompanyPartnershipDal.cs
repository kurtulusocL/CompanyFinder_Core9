using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface ICompanyPartnershipDal : IEntityRepository<CompanyPartnership>
    {
        Task<bool> SetAvailablePartnership(int id);
        Task<bool> SetNotAvailablePartnership(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
