using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AppointmentAnswerManager : IAppointmentAnswerService
    {
        readonly IAppointmentAnswerDal _appointmentAnswerDal;
        public AppointmentAnswerManager(IAppointmentAnswerDal appointmentAnswerDal)
        {
            _appointmentAnswerDal = appointmentAnswerDal;
        }

        public async Task<bool> CreateAsync(string title, string desc, DateTime reAppointmentDate, int? appointmentId)
        {
            try
            {
                if (appointmentId == null)
                    throw new ArgumentNullException(nameof(appointmentId), "Appointment ID was null");

                var entity = new AppointmentAnswer
                {
                    Title = title,
                    Desc = desc,
                    ReAppointmentDate = reAppointmentDate,
                    AppointmentId = appointmentId
                };
                if (entity != null)
                {
                    var result = await _appointmentAnswerDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AppointmentAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _appointmentAnswerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _appointmentAnswerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _appointmentAnswerDal.GetAllIncludeAsync(new Expression<Func<AppointmentAnswer, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, null, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByAppointmentIdAsync(int? appointmentId)
        {
            try
            {
                if (appointmentId == null)
                    throw new ArgumentNullException(nameof(appointmentId), "Appointment ID was null");

                var result = await _appointmentAnswerDal.GetAllIncludeByIdAsync(appointmentId, "AppointmentId", new Expression<Func<AppointmentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByAppointmentIdForAdminAsync(int? appointmentId)
        {
            try
            {
                if (appointmentId == null)
                    throw new ArgumentNullException(nameof(appointmentId), "Appointment ID was null");

                var result = await _appointmentAnswerDal.GetAllIncludeByIdAsync(appointmentId, "AppointmentId", new Expression<Func<AppointmentAnswer, bool>>[]
                {

                }, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByApprovedAsync()
        {
            try
            {
                var result = await _appointmentAnswerDal.GetAllIncludeAsync(new Expression<Func<AppointmentAnswer, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.ReAppointmentApproved==true
                }, null, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingByNotApprovedAsync()
        {
            try
            {
                var result = await _appointmentAnswerDal.GetAllIncludeAsync(new Expression<Func<AppointmentAnswer, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.ReAppointmentApproved==false
                }, null, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<IEnumerable<AppointmentAnswer>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _appointmentAnswerDal.GetAllIncludeAsync(new Expression<Func<AppointmentAnswer, bool>>[] {

                }, null, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppointmentAnswer>();
            }
        }

        public async Task<AppointmentAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _appointmentAnswerDal.GetIncludeAsync(i => i.Id == id, y => y.Appointment, y => y.Appointment.User, y => y.Appointment.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetApprovedAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetApprovedAsync(id);
            return result;
        }
        public async Task<bool> SetNotApprovedAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetNotApprovedAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _appointmentAnswerDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string desc, DateTime reAppointmentDate, int? appointmentId, int id)
        {
            try
            {
                if (appointmentId == null)
                    throw new ArgumentNullException(nameof(appointmentId), "Appointment ID was null");

                var entity = new AppointmentAnswer
                {
                    Title = title,
                    Desc = desc,
                    ReAppointmentDate = reAppointmentDate,
                    AppointmentId = appointmentId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _appointmentAnswerDal.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }        
    }
}
