using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategory>> GetAllIncludingAsync();
        Task<IEnumerable<BlogCategory>> GetAllIncludingForAddBlogAsync();
        Task<IEnumerable<BlogCategory>> GetAllIncludingForAdmin();
        Task<BlogCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(BlogCategory entity);
        Task<bool> UpdateAsync(BlogCategory entity);
        Task<bool> DeleteAsync(BlogCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<BlogCategory> GetAllSiteMap();
        IEnumerable<BlogCategory> GetAllIncludingBlogCategories();
    }
}
