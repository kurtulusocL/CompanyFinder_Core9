using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICustomerStatusService
    {
        Task<IEnumerable<CustomerStatus>> GetAllIncludingAsync();
        Task<IEnumerable<CustomerStatus>> GetAllIncludingForAddCustomerAsync();
        Task<IEnumerable<CustomerStatus>> GetAllIncludingForAdminAsync();
        Task<CustomerStatus> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CustomerStatus entity);
        Task<bool> UpdateAsync(CustomerStatus entity);
        Task<bool> DeleteAsync(CustomerStatus entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
