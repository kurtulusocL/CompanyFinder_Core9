using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAuditService
    {
        Task<IEnumerable<Audit>> GetAllAsync();
        Task<IEnumerable<Audit>> GetAllVisitorAuditAsync();
        Task<IEnumerable<Audit>> GetAllUserAuditAsync();
        Task<IEnumerable<Audit>> GetAllForAdmin();
        Task<Audit> GetByIdAsync(int? id);     
        Task<bool> DeleteAsync(Audit entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
