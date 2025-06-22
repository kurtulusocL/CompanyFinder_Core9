using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IStockService
    {
        Task<IEnumerable<Stock>> GetAllIncludingAsync();
        Task<IEnumerable<Stock>> GetAllIncludingByQuantityAsync();
        Task<IEnumerable<Stock>> GetAllIncludingByProductIdAsync(int? productId);
        Task<IEnumerable<Stock>> GetAllIncludingByCompanyIdIdAsync(int? productId);
        Task<IEnumerable<Stock>> GetAllIncludingForAdmin();
        Task<Stock> GetByIdAsync(int? id);
        Task<bool> CreateAsync(int quantity, string? warehouse, int? productId);
        Task<bool> UpdateAsync(int quantity, string? warehouse, int? productId, int id);
        Task<bool> DeleteAsync(Stock entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Stock GetProductForCompanyUserSrockByProductId(int? productId);
        Task<Stock> GetProductForCompanyUserStockByProductIdAsync(int? productId);
    }
}
