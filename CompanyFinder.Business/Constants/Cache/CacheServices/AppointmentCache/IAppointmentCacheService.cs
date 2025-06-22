namespace CompanyFinder.Business.Constants.Cache.CacheServices.AppointmentCache
{
    public interface IAppointmentCacheService
    {
        Task AppointmentUpdateCache(int? companyId, int? id, string? userId);
    }
}
