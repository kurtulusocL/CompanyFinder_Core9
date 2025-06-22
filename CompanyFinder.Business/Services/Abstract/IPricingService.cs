using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IPricingService
    {
        Task<IEnumerable<Pricing>> GettAllIncludingAsync();
        Task<IEnumerable<Pricing>> GettAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Pricing>> GettAllIncludingForAdminAsync();
        Task<Pricing> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Pricing entity);
        Task<bool>UpdateAsync(Pricing entity);
        Task<bool> DeleteAsync(Pricing entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
