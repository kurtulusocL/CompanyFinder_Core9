using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICancelMembershipService
    {
        Task<IEnumerable<CancelMembership>> GetAllIncludingAsync();
        Task<IEnumerable<CancelMembership>> GetAllIncludingCancelledAsync();
        Task<IEnumerable<CancelMembership>> GetAllIncludingNotCancelledAsync();
        Task<IEnumerable<CancelMembership>> GetAllIncludingByReasonIdAsync(int id);
        Task<IEnumerable<CancelMembership>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<CancelMembership>> GetAllIncludingForAdmin();
        Task<IEnumerable<CancelMembership>> GetAllIncludingCancelledForAdminAsync();
        Task<IEnumerable<CancelMembership>> GetAllIncludingForAdminByReasonIdAsync(int id);
        Task<IEnumerable<CancelMembership>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<CancelMembership> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string? otherReason, DateTime expectedCancelDate, int cancelMembershipReasonId, string userId);
        Task<bool> DeleteAsync(CancelMembership entity, int id);
        Task<bool> SetCancelledAsync(int id);
        Task<bool> SetNotCancelledAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<CancelMembership> GetAllIncludingCancelMembershipsForAdminHeader();
    }
}
