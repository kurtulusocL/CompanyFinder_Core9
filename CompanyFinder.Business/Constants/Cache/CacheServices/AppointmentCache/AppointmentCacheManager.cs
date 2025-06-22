using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.AppointmentCache
{
    public class AppointmentCacheManager : IAppointmentCacheService
    {
        private readonly IMemoryCache _memoryCache;
        readonly IAppointmentDal _appointmentDal;
        public AppointmentCacheManager(IMemoryCache memoryCache, IAppointmentDal appointmentDal)
        {
            _memoryCache = memoryCache;
            _appointmentDal = appointmentDal;
        }
        public async Task AppointmentUpdateCache(int? companyId, int? id, string? userId)
        {
            if (_memoryCache.TryGetValue(AppointmentCacheKeys.AllAppointments, out List<Appointment> allAppointments))
            {
                var updatedAppointments = await _appointmentDal.GetAllAsync();
                _memoryCache.Set(AppointmentCacheKeys.AllAppointments, updatedAppointments, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(AppointmentCacheKeys.AllApprovedAppointments, out List<Appointment> approvedAppointments))
            {
                var updatedApprovedAppointments = await _appointmentDal.GetAllAsync(a => a.IsApproved);
                _memoryCache.Set(AppointmentCacheKeys.AllApprovedAppointments, updatedApprovedAppointments, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(AppointmentCacheKeys.AllNotApprovedAppointments, out List<Appointment> notApprovedAppointments))
            {
                var updatedNotApprovedAppointments = await _appointmentDal.GetAllAsync(a => !a.IsApproved);
                _memoryCache.Set(AppointmentCacheKeys.AllNotApprovedAppointments, updatedNotApprovedAppointments, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(AppointmentCacheKeys.AllAppointmentsForAdmin, out List<Appointment> adminAppointments))
            {
                var updatedAdminAppointments = await _appointmentDal.GetAllAsync();
                _memoryCache.Set(AppointmentCacheKeys.AllAppointmentsForAdmin, updatedAdminAppointments, TimeSpan.FromMinutes(25));
            }

            if (companyId.HasValue)
            {
                string companyCacheKey = AppointmentCacheKeys.AllAppointmentsByCompanyId(companyId.Value);
                if (_memoryCache.TryGetValue(companyCacheKey, out List<Appointment> companyAppointments))
                {
                    var updatedCompanyAppointments = await _appointmentDal.GetAllAsync(a => a.CompanyId == companyId.Value);
                    _memoryCache.Set(companyCacheKey, updatedCompanyAppointments, TimeSpan.FromMinutes(25));
                }

                string companyAdminCacheKey = AppointmentCacheKeys.AllAppointmentsByCompanyIdForAdmin(companyId.Value);
                if (_memoryCache.TryGetValue(companyAdminCacheKey, out List<Appointment> companyAdminAppointments))
                {
                    var updatedCompanyAdminAppointments = await _appointmentDal.GetAllAsync(a => a.CompanyId == companyId.Value);
                    _memoryCache.Set(companyAdminCacheKey, updatedCompanyAdminAppointments, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = AppointmentCacheKeys.AllAppointmentsByUserId(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Appointment> userAppointments))
                {
                    var updatedUserAppointments = await _appointmentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserAppointments, TimeSpan.FromMinutes(30));
                }

                string userAdminCacheKey = AppointmentCacheKeys.AllAppointmentsByUserIdForAdmin(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Appointment> userAdminAppointments))
                {
                    var updatedUserAdminAppointments = await _appointmentDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminAppointments, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string appointmentCacheKey = AppointmentCacheKeys.AppointmentById(id.Value);
                if (_memoryCache.TryGetValue(appointmentCacheKey, out Appointment singleAppointment))
                {
                    var updatedAppointment = await _appointmentDal.GetAsync(a => a.Id == id.Value);
                    if (updatedAppointment != null)
                    {
                        _memoryCache.Set(appointmentCacheKey, updatedAppointment, TimeSpan.FromMinutes(25));
                    }
                }
            }

            //_memoryCache.Remove("GetAllAppointments");
            //_memoryCache.Remove("GetAllAppointmentsByApproved");
            //_memoryCache.Remove("GetAllAppointmentsByNotApproved");
            //_memoryCache.Remove("GetAllIncludingAppointmentForAdmin");

            //if (companyId.HasValue)
            //{
            //    string companyCacheKey = $"GetAllIncludingAppointmentByCompanyId_{companyId}";
            //    _memoryCache.Remove(companyCacheKey);

            //    string companyAdminCacheKey = $"GetAllIncludingAppointmentByCompanyIdForAdmin_{companyId}";
            //    _memoryCache.Remove(companyAdminCacheKey);
            //}

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    string companyCacheKey = $"GetAllIncludingAppointmentByUserId_{userId}";
            //    _memoryCache.Remove(companyCacheKey);

            //    string companyUserCacheKey = $"GetAllIncludingAppointmentByUserIdForAdmin_{userId}";
            //    _memoryCache.Remove(companyUserCacheKey);
            //}

            //if (id.HasValue)
            //{
            //    string companyCacheKey = $"GetAppointmentById_{id}";
            //    _memoryCache.Remove(companyCacheKey);
            //}
        }

    }
}
