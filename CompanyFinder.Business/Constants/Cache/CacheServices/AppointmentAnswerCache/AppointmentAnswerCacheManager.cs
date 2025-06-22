using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.AppointmentAnswerCache
{
    public class AppointmentAnswerCacheManager : IAppointmentAnswerCacheService
    {
        private readonly IMemoryCache _memoryCache;
        readonly IAppointmentAnswerDal _appointmentAnswerDal;       
        public AppointmentAnswerCacheManager(IMemoryCache memoryCache, IAppointmentAnswerDal appointmentAnswerDal)
        {
            _memoryCache = memoryCache;
            _appointmentAnswerDal = appointmentAnswerDal;
        }

        public async Task AppointmentUpdateCache(int? appointmentId, int? id)
        {
            if (_memoryCache.TryGetValue(AppointmentAnswerCacheKeys.AllAppointmentAnswers, out List<AppointmentAnswer> allAppointments))
            {
                var updatedAppointments = await _appointmentAnswerDal.GetAllAsync();
                _memoryCache.Set(AppointmentAnswerCacheKeys.AllAppointmentAnswers, updatedAppointments, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(AppointmentAnswerCacheKeys.AllApprovedAnswers, out List<AppointmentAnswer> approvedAppointments))
            {
                var updatedApprovedAppointments = await _appointmentAnswerDal.GetAllAsync(a => a.ReAppointmentApproved);
                _memoryCache.Set(AppointmentAnswerCacheKeys.AllApprovedAnswers, updatedApprovedAppointments, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(AppointmentAnswerCacheKeys.AllNotApprovedAnswers, out List<AppointmentAnswer> notApprovedAppointments))
            {
                var updatedNotApprovedAppointments = await _appointmentAnswerDal.GetAllAsync(a => !a.ReAppointmentApproved);
                _memoryCache.Set(AppointmentAnswerCacheKeys.AllNotApprovedAnswers, updatedNotApprovedAppointments, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(AppointmentAnswerCacheKeys.AllAnswersForAdmin, out List<AppointmentAnswer> adminAppointments))
            {
                var updatedAdminAppointments = await _appointmentAnswerDal.GetAllAsync();
                _memoryCache.Set(AppointmentAnswerCacheKeys.AllAnswersForAdmin, updatedAdminAppointments, TimeSpan.FromMinutes(25));
            }

            if (appointmentId.HasValue)
            {
                string companyCacheKey = AppointmentAnswerCacheKeys.GetAllIncludingByAppointmentIdAsync(appointmentId.Value);
                if (_memoryCache.TryGetValue(companyCacheKey, out List<Appointment> companyAppointments))
                {
                    var updatedCompanyAppointments = await _appointmentAnswerDal.GetAllAsync(a => a.AppointmentId == appointmentId.Value);
                    _memoryCache.Set(companyCacheKey, updatedCompanyAppointments, TimeSpan.FromMinutes(25));
                }

                string companyAdminCacheKey = AppointmentAnswerCacheKeys.GetAllIncludingByAppointmentIdForAdminAsync(appointmentId.Value);
                if (_memoryCache.TryGetValue(companyAdminCacheKey, out List<Appointment> companyAdminAppointments))
                {
                    var updatedCompanyAdminAppointments = await _appointmentAnswerDal.GetAllAsync(a => a.AppointmentId == appointmentId.Value);
                    _memoryCache.Set(companyAdminCacheKey, updatedCompanyAdminAppointments, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string appointmentCacheKey = AppointmentAnswerCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(appointmentCacheKey, out Appointment singleAppointment))
                {
                    var updatedAppointment = await _appointmentAnswerDal.GetAsync(a => a.Id == id.Value);
                    if (updatedAppointment != null)
                    {
                        _memoryCache.Set(appointmentCacheKey, updatedAppointment, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
