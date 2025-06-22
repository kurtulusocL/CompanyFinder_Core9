using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IBlackListService
    {
        Task<IEnumerable<BlackList>> GetAllIncludingAsync();
        Task<IEnumerable<BlackList>> GetAllIncludingByAuditIdAsync(int? auditId);
        Task<IEnumerable<BlackList>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<BlackList>> GetAllIncludingForAdminByAuditIdAsync(int? auditId);
        Task<BlackList> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId);
        Task<bool> UpdateAsync(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId, int id);
        Task<bool> DeleteAsync(BlackList entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
