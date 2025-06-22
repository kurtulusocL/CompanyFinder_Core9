namespace CompanyFinder.Business.Constants.Cache.CacheServices.AppointmentAnswerCache
{
    public interface IAppointmentAnswerCacheService
    {
        Task AppointmentUpdateCache(int? appointmentId, int? id);
    }
}
