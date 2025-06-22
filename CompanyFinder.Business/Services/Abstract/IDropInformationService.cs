using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IDropInformationService
    {
        Task<IEnumerable<DropInformation>> GetAllAsync();
        Task<IEnumerable<DropInformation>> GetAllForAdmin();
        Task<DropInformation> GetByIdAsync(int? id);
        bool Create(DropInformation entity);
        Task<bool> DeleteAsync(DropInformation entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<DropInformation> GetAllDropInformationsForAdminHeader();
    }
}
