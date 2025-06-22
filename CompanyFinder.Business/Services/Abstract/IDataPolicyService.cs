using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IDataPolicyService
    {
        Task<IEnumerable<DataPolicy>> GetAllAsync();
        Task<IEnumerable<DataPolicy>> GetAllForAdmin();
        Task<DataPolicy> GetByIdAsync(int? id);
        Task<bool> CreateAsync(DataPolicy entity);
        Task<bool> UpdateAsync(DataPolicy entity);
        Task<bool> DeleteAsync(DataPolicy entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
