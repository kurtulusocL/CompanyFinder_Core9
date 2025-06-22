using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllIncludingAsync();
        Task<IEnumerable<Appointment>> GetAllIncludingByApprovedAsync();
        Task<IEnumerable<Appointment>> GetAllIncludingByNotApprovedAsync();
        Task<IEnumerable<Appointment>> GetAllIncludingByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Appointment>> GetAllIncludingByUserIdAsync(string userId);
        Task<IEnumerable<Appointment>> GetAllIncludingForAdmin();
        Task<IEnumerable<Appointment>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId);
        Task<IEnumerable<Appointment>> GetAllIncludingByUserIdForAdminAsync(string userId);
        Task<Appointment> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string subject, string desc, DateTime appointmentDate, int? companyId, string userId);
        Task<bool> UpdateAsync(string subject, string desc, DateTime appointmentDate, int? companyId, string userId, int id);
        Task<bool> DeleteAsync(Appointment entity, int id);
        Task<bool> SetApprovedAsync(int id);
        Task<bool> SetNotApprovedAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        int AppointmentCounter();
        Task<IEnumerable<Appointment>> GetAllIncludingAppointmentForCompanyUserByUserId(string userId);
        Task<IEnumerable<Appointment>> GetAllIncludingCompanyAppointmentForCompanyUserByUserIdAsync(string userId);
    }
}
