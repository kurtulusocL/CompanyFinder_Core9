using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IFormCategoryService
    {
        Task<IEnumerable<FormCategory>> GetAllIncludingAsync();
        Task<IEnumerable<FormCategory>> GetAllIncludingForAddAsync();
        Task<IEnumerable<FormCategory>> GetAllIncludingForAdminAsync();
        Task<FormCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(FormCategory entity);
        Task<bool> UpdateAsync(FormCategory entity);
        Task<bool> DeleteAsync(FormCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<FormCategory> GetAllSitemap();
        IEnumerable<FormCategory> GetAllIncludingFormCategoriesForSidebar();
    }
}
