using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICancelMembershipReasonService
    {
        Task<IEnumerable<CancelMembershipReason>> GetAllIncludingAsync();
        Task<IEnumerable<CancelMembershipReason>> GetAllIncludingForAddAsync();
        Task<IEnumerable<CancelMembershipReason>> GetAllIncludingForAdmin();
        Task<CancelMembershipReason> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CancelMembershipReason entity);
        Task<bool> UpdateAsync(CancelMembershipReason entity);
        Task<bool> DeleteAsync(CancelMembershipReason entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
