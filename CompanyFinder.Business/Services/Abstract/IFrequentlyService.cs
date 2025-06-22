using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IFrequentlyService
    {
        Task<IEnumerable<Frequently>> GetAllAsync();
        Task<IEnumerable<Frequently>> GetAllForAdmin();
        Task<Frequently> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Frequently entity);
        Task<bool> UpdateAsync(Frequently entity);
        Task<bool> DeleteAsync(Frequently entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Frequently> GetAllSiteMap();
    }
}
