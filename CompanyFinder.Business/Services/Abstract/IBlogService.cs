using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllIncludingAsync();
        Task<IEnumerable<Blog>> GetAllIncludingByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Blog>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Blog>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingForAdmin();
        Task<Blog> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, IFormFile image);
        Task<bool> UpdateAsync(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, int id, IFormFile image);
        Task<bool> DeleteAsync(Blog entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int BlogCounter();
        IEnumerable<Blog> GetAllSiteMap();
        IEnumerable<Blog> GetAllIncludingCompanyBlogs(int? companyId);
        Blog GetIncludingBlogForHeaderByCompanyId(int? companyId);
        Blog GetIncludeBlogCompanyInfoForBlogDetailByCompanyId(int? companyId);
        IEnumerable<Blog> GetAllIncludingRandomBlog();
        Blog GetBlogById(int? id);
        IEnumerable<Blog> GetAllIncludingRandomBlogForHome();
        Task<IEnumerable<Blog>> GetAllIncludingMostLikedBlogsAsync();
        Task<IEnumerable<Blog>> GetAllIncludingMostHitBlogsAsync();
        Task<IEnumerable<Blog>> GetAllIncludingMostSavedBlogsAsync();
        Task<IEnumerable<Blog>> GetAllIncludingMostCommentBlogsAsync();
        Task<IEnumerable<Blog>> GetAllIncludingYourMostLikedBlogsAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingYourMostHitBlogsAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingYourMostSavedBlogsAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingYourMostCommentBlogsAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingBlogLikesForCompanyByUserIdAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingBlogHitForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Blog>> GetAllIncludingSavedBlogsForSaveContentBlogByUserIdAsync(string userId);
    }
}
