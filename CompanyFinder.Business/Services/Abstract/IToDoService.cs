using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IToDoService
    {
        Task<IEnumerable<ToDo>> GetAllIncludingAsync();
        Task<IEnumerable<ToDo>> GetAllIncludingOpenTaskAsync();
        Task<IEnumerable<ToDo>> GetAllIncludingFinishedAsync();
        Task<IEnumerable<ToDo>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<ToDo>> GetAllIncludingFinishedTaskByUserIdAsync(string userId);
        Task<IEnumerable<ToDo>> GetAllIncludingOpenTaskByUserIdAsync(string userId);
        Task<IEnumerable<ToDo>> GetAllIncludingForAdmin();
        Task<ToDo> GetByIdAsync(int? id);
        ToDo HitRead(int? id);
        Task<bool> CreateAsync(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId);
        Task<bool> UpdateAsync(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId, int id);
        Task<bool> DeleteAsync(ToDo entity, int id);
        Task<bool> SetFinishedAsync(int id);
        Task<bool> SetNotFinishedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IEnumerable<ToDo> GetAllIncludingTaksForAdminProfileByUserId(string userId);
    }
}
