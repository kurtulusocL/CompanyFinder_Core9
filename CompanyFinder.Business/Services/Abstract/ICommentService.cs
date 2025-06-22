using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllIncludingAsync();
        Task<IEnumerable<Comment>> GetAllIncludingByBlogIdAsync(int? blogId);
        Task<IEnumerable<Comment>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Comment>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Comment>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllIncludingForAdmin();
        Task<IEnumerable<Comment>> GetAllIncludingByBlogIdForAdminAsync(int? blogId);
        Task<IEnumerable<Comment>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId);
        Task<IEnumerable<Comment>> GetAllIncludingByProductIdForAdminAsync(int? productId);
        Task<IEnumerable<Comment>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<Comment> GetByIdAsync(int? id);
        Task<bool> CreateBlogCommentAsync(string? subject, string text, int? blogId, string userId);
        Task<bool> CreateCompanyCommentAsync(string? subject, string text, int? companyId, string userId);
        Task<bool> CreateProductCommentAsync(string? subject, string text, int? productId, string userId);
        Task<bool> DeleteAsync(Comment entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int CommentCounter();
        IEnumerable<Comment> GetAllSiteMap();
        IEnumerable<Comment> GetAllIncludingCommentForCompanyProductByProductId(int? productId);
        IEnumerable<Comment> GetAllIncludingCommentForCompanyByCompanyId(int? companyId);
        IEnumerable<Comment> GetAllIncludingCommentForCompanyBlogByBlogId(int? blogId);
        Comment GetCommentById(int? id);
        Task<IEnumerable<Comment>> GetAllIncludingCommentForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllIncludingCompanyCommentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllIncludingProductCommentsForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllIncludingBlogCommentsForCompanyUserByUserIdAsync(string userId);
        IEnumerable<Comment> GetAllIncludingCommentsForExplorerBlogDetailByBlogId(int? blogId);
        IEnumerable<Comment> GetAllIncludingCommentsForExplorerCompanyDetailByCompanyId(int? companyId);
        IEnumerable<Comment> GetAllIncludingCommentsForExplorerProductDetailByProductId(int? productId);
    }
}
