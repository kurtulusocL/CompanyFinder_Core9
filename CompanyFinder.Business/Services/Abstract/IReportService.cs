using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllIncludingAsync();
        Task<IEnumerable<Report>> GetAllIncludingNotAnsweredAsync();
        Task<IEnumerable<Report>> GetAllIncludingAnsweredAsync();
        Task<IEnumerable<Report>> GetAllIncludingNotFixedReportsAsync();
        Task<IEnumerable<Report>> GetAllIncludingFixedReportsAsync();
        Task<IEnumerable<Report>> GetAllIncludingReportByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Report>> GetAllIncludingReportByBlogIdAsync(int? blogId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCommentIdAsync(int? commentId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCommentAnswerIdAsync(int? commentAnswerId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCompanyPartnershipIdAsync(int? companyPartnershipId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCompanyFormIdAsync(int? companyFormId);
        Task<IEnumerable<Report>> GetAllIncludingReportByCompanyFormAnswerIdAsync(int? companyFormAnswerId);
        Task<IEnumerable<Report>> GetAllIncludingReportByProductIdAsync(int? productId);
        Task<IEnumerable<Report>> GetAllIncludingReportByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllIncludingReportForAdminByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllIncludingForAdmin();
        Task<Report> GetByIdAsync(int? id);
        Report HitRead(int? id);
        Task<bool> CreateBlogReportAsync(string title, string desc, int reportCategoryId, int? blogId, string userId);
        Task<bool> CreateCommentReportAsync(string title, string desc, int reportCategoryId, int? commentId, string userId);
        Task<bool> CreateCommentAnswerReportAsync(string title, string desc, int reportCategoryId, int? commentAnswerId, string userId);
        Task<bool> CreateCompanyReportAsync(string title, string desc, int reportCategoryId, int? companyId, string userId);
        Task<bool> CreateCompanyPartnershipReportAsync(string title, string desc, int reportCategoryId, int? companyPartnershipId, string userId);
        Task<bool> CreateCompanyFormReportAsync(string title, string desc, int reportCategoryId, int? companyFormId, string userId);
        Task<bool> CreateCompanyFormAnswerReportAsync(string title, string desc, int reportCategoryId, int? companyFormAnswerId, string userId);
        Task<bool> CreateProductReportAsync(string title, string desc, int reportCategoryId, int? productId, string userId);
        Task<bool> DeleteAsync(Report entity, int id);
        Task<bool> SetFixedAsync(int id);
        Task<bool> SetNotFixedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Report> GetAllIncludingReportsForAdminHeader();
        int ReportCounter();
        IEnumerable<Report> GetAllIncludingProductReportForCompanyUserByProductId(int? productId);
        IEnumerable<Report> GetAllIncludingCompanyReportForCompanyUserByCompanyId(int? companyId);
        IEnumerable<Report> GetAllIncludingBlogReportForCompanyUserByBlogId(int? blogId);
        Task<IEnumerable<Report>> GetAllIncludingReportForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllIncludingCompanyReportForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllIncludingProductReportForCompanyUserByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllIncludingBlogReportForCompanyUserByUserIdAsync(string userId);
        IEnumerable<Report> GetAllIncludingPartnershipReportForCompanyUserByCompanyPartnershipId(int? companyPartnershipId);
    }
}
