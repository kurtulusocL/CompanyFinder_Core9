using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IPricingCategoryService
    {
        Task<IEnumerable<PricingCategory>> GettAllIncludingAsync();
        Task<IEnumerable<PricingCategory>> GettAllIncludingForAddPricingAsync();
        Task<IEnumerable<PricingCategory>> GettAllIncludingForAdminAsync();
        Task<PricingCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(PricingCategory entity);
        Task<bool> UpdateAsync(PricingCategory entity);
        Task<bool> DeleteAsync(PricingCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
