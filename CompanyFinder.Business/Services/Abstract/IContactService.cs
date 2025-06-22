using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> GetAllForAdmin();
        Task<Contact> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Contact entity);
        Task<bool> UpdateAsync(Contact entity);
        Task<bool> DeleteAsync(Contact entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<Contact> GetAllContactForHomeHeader();
        IEnumerable<Contact> GetAllSiteMap();
    }
}
