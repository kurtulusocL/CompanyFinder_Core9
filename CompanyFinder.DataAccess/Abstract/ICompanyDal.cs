using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesFortAdminAsync();
        int CompanyCounter();
        Task<bool> SetCommentableAsync(int id);
        Task<bool> SetNotCommentableAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}
