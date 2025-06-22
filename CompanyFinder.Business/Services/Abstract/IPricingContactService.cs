using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IPricingContactService
    {
        Task<IEnumerable<PricingContact>> GetAllIncludingAsync();
        Task<IEnumerable<PricingContact>> GetAllIncludingByPricingId(int? pricingId);
        Task<IEnumerable<PricingContact>> GetAllIncludingForAdmin();
        Task<PricingContact> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string nameSurname, string companyName, string? email, string phoneNumber, string desc, int? pricingId);       
        Task<bool> DeleteAsync(PricingContact entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
