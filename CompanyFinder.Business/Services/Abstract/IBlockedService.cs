using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IBlockedService
    {
        Task<IEnumerable<Blocked>> GetAllAsync();
        Task<IEnumerable<Blocked>> GetAllForAdminAsync();
        Task<Blocked> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Blocked entity);
        Task<bool> UpdateAsync(Blocked entity);
        Task<bool> DeleteAsync(Blocked entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
