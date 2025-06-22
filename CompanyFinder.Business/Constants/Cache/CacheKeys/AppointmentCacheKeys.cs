namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class AppointmentCacheKeys
    {
        public const string AllAppointments = "GetAllAppointments";
        public const string AllApprovedAppointments = "GetAllAppointmentsByApproved";
        public const string AllNotApprovedAppointments = "GetAllAppointmentsByNotApproved";
        public const string AllAppointmentsForAdmin = "GetAllIncludingAppointmentForAdmin";

        public static string AllAppointmentsByCompanyId(int companyId) => $"GetAllIncludingAppointmentByCompanyId_{companyId}";
        public static string AllAppointmentsByCompanyIdForAdmin(int companyId) => $"GetAllIncludingAppointmentByCompanyIdForAdmin_{companyId}";
        public static string AllAppointmentsByUserId(string userId) => $"GetAllIncludingAppointmentByUserId_{userId}";
        public static string AllAppointmentsByUserIdForAdmin(string userId) => $"GetAllIncludingAppointmentByUserIdForAdmin_{userId}";
        public static string AppointmentById(int id) => $"GetAppointmentById_{id}";
    }
}
