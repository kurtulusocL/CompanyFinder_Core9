using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAppointmentAnswerService
    {
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingAsync();
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByApprovedAsync();
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByNotApprovedAsync();
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByAppointmentIdAsync(int? appointmentId);
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingForAdmin();
        Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByAppointmentIdForAdminAsync(int? appointmentId);
        Task<AppointmentAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string title, string desc, DateTime reAppointmentDate, int? appointmentId);
        Task<bool> UpdateAsync(string title, string desc, DateTime reAppointmentDate, int? appointmentId, int id);
        Task<bool> DeleteAsync(AppointmentAnswer entity, int id);
        Task<bool> SetApprovedAsync(int id);
        Task<bool> SetNotApprovedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);       
    }
}
