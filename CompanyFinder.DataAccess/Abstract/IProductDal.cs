using CompanyFinder.Core.DataAccess;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsAsync();
        Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsForAdminAsync();
        int ProductCounter();
        Task<bool> SetAvailableAsync(int id);
        Task<bool> SetNotAvailableAsync(int id);
        Task<bool> SetQuestionableAsync(int id);
        Task<bool> SetNotQuestionableAsync(int id);
        Task<bool> SetCommentableAsync(int id);
        Task<bool> SetNotCommentableAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
    }
}
