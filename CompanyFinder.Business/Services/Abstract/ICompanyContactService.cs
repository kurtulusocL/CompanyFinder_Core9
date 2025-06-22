using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyContactService
    {
        Task<IEnumerable<CompanyContact>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyContact>> GetAllIncludingContactHideAsync();
        Task<IEnumerable<CompanyContact>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyContact>> GetAllIncludingForAdmin();
        Task<IEnumerable<CompanyContact>> GetAllIncludingForAdminByCompanyIdAsync(int? companyId);
        Task<CompanyContact> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId);
        Task<bool> UpdateAsync(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId, int id);
        Task<bool> DeleteAsync(CompanyContact entity, int id);
        Task<bool> SetHideAsync(int id);
        Task<bool> SetNotHideAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CompanyContact GetCompanyContactForcCompanyByCompanyId(int? companyId);
        CompanyContact GetCompanyContactByCompanyId(int? companyId);
    }
}
