using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllIncludingAsync();
        Task<IEnumerable<Customer>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Customer>> GetAllIncludingByStatusIdAsync(int customerStatusId);
        Task<IEnumerable<Customer>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Customer>> GetAllIncludingForCompanyUserByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Customer>> GetAllIncludingForCompanyUserByStatusIdAsync(int customerStatusId);
        Task<Customer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId);
        Task<bool> UpdateAsync(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId, int id);
        Task<bool> DeleteAsync(Customer entity, int id);      
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);       
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
