using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IPictureService
    {
        Task<IEnumerable<Picture>> GetAllIncludingAsync();
        Task<IEnumerable<Picture>> GetAllIncludingByBlogIdAsync(int? blogId);
        Task<IEnumerable<Picture>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Picture>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Picture>> GetAllIncludingForAdmin();
        Task<IEnumerable<Picture>> GetAllIncludingByBlogIdForAdminAsync(int? blogId);
        Task<IEnumerable<Picture>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId);
        Task<IEnumerable<Picture>> GetAllIncludingByProductIdForAdminAsync(int? productId);
        Task<Picture> GetByIdAsync(int? id);
        IEnumerable<Picture> GetAllIncludingByBlogId(int? blogId);
        IEnumerable<Picture> GetAllIncludingByCompanyId(int? companyId);
        IEnumerable<Picture> GetAllIncludingByProductId(int? productId);
        Task<bool> CreateBlogImagesAsync(int? blogId, IEnumerable<IFormFile> images);
        Task<bool> CreateCompanyImagesAsync(int? companyId, IEnumerable<IFormFile> images);
        Task<bool> CreateProductImagesAsync(int? productId, IEnumerable<IFormFile> images);
        Task<bool> UpdateBlogImageAsync(int? blogId, int id, IFormFile image);
        Task<bool> UpdateCompanyImageAsync(int? companyId, int id, IFormFile image);
        Task<bool> UpdateProductImageAsync(int? productId, int id, IFormFile image);
        Task<bool> DeleteAsync(Picture entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int PictureCounter();
        Task<IEnumerable<Picture>> GetAllIncludingProductImagesForCompanyUserByProductIdAsync(int? productId);
        Task<IEnumerable<Picture>> GetAllIncludingCompanyImagesForCompanyUserByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Picture>> GetAllIncludingBlogImagesForCompanyUserByBlogIdAsync(int? blogId);
        IEnumerable<Picture> GetAllIncludingExplorerBlogDetailImageByBlogId(int? blogId);
        IEnumerable<Picture> GetAllIncludingExplorerProductDetailImageByProductId(int? productId);
        IEnumerable<Picture> GetAllIncludingExplorerCompanyDetailImageByCompanyId(int? companyId);
    }
}
