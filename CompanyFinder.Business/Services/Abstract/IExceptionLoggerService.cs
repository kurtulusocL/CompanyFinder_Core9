using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IExceptionLoggerService
    {
        Task<IEnumerable<ExceptionLogger>> GetAllAsync();
        Task<IEnumerable<ExceptionLogger>> GetAllForAdmin();
        Task<ExceptionLogger> GetByIdAsync(int? id);       
        Task<bool> DeleteAsync(ExceptionLogger entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
