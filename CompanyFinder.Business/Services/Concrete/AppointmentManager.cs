using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AppointmentManager : IAppointmentService
    {
        readonly IAppointmentDal _appointmentDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentManager(IAppointmentDal appointmentDal, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentDal = appointmentDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateAsync(string subject, string desc, DateTime appointmentDate, int? companyId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                if (companyId == null)
                    throw new ArgumentNullException(nameof(userId), "companyId was null");

                var entity = new Appointment
                {
                    Subject = subject,
                    Desc = desc,
                    AppointmentDate = appointmentDate,
                    CompanyId = companyId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _appointmentDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Appointment entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _appointmentDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _appointmentDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _appointmentDal.GetAllIncludeAsync(new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByApprovedAsync()
        {
            try
            {
                var result = await _appointmentDal.GetAllIncludeAsync(new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false,
                    y=>y.IsApproved==true
                }, null, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "Company Id was null");

                var result = await _appointmentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "Company Id was null");

                var result = await _appointmentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Appointment, bool>>[]
                {

                }, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByNotApprovedAsync()
        {
            try
            {
                var result = await _appointmentDal.GetAllIncludeAsync(new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false,
                    y=>y.IsApproved==false
                }, null, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _appointmentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _appointmentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Appointment, bool>>[]
                {

                }, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _appointmentDal.GetAllIncludeAsync(new Expression<Func<Appointment, bool>>[]
                {

                }, null, y => y.User, y => y.Company, y => y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<Appointment> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

               return await _appointmentDal.GetIncludeAsync(i => i.Id == id, i => i.User, i => i.Company, i => i.AppointmentAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetApprovedAsync(int id)
        {
            var result = await _appointmentDal.SetApprovedAsync(id);
            return result;
        }
        public async Task<bool> SetNotApprovedAsync(int id)
        {
            var result = await _appointmentDal.SetNotApprovedAsync(id);
            return result;
        }
        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _appointmentDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _appointmentDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _appointmentDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _appointmentDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string subject, string desc, DateTime appointmentDate, int? companyId, string userId, int id)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new Appointment
                {
                    Subject = subject,
                    Desc = desc,
                    AppointmentDate = appointmentDate,
                    CompanyId = companyId,
                    UserId = userId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _appointmentDal.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public int AppointmentCounter()
        {
            var result = _appointmentDal.AppointmentCounter();
            return result;
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingAppointmentForCompanyUserByUserId(string userId)
        {
            try
            {
                if(userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _appointmentDal.GetAllIncludeByIdAsync(userId, "userId", new Expression<Func<Appointment, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false,
                   i=>i.UserId==userId
                }, y => y.Company, y=>y.User, y=>y.Company.City, y=>y.Company.Country, y=>y.AppointmentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllIncludingCompanyAppointmentForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _appointmentDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<Appointment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.UserId==userId
                }, y => y.Company, y => y.User, y=>y.AppointmentAnswers);
                return result.OrderByDescending(i => i.AppointmentDate).ToList();
            }
            catch (Exception)
            {
                return new List<Appointment>();
            }
        }
    }
}
