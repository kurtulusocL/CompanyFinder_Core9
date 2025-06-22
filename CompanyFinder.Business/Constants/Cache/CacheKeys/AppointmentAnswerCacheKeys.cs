using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheKeys
{
    public static class AppointmentAnswerCacheKeys
    {
        public const string AllAppointmentAnswers = "GetAllAppointmentAnswers";
        public const string AllApprovedAnswers = "GetAllIncludingByApprovedAnswers";
        public const string AllNotApprovedAnswers = "GetAllIncludingByNotApprovedAnswers";
        public const string AllAnswersForAdmin = "GetAllIncludingForAdminAnswers";

        public static string GetAllIncludingByAppointmentIdAsync(int? appointmentId) => $"GetAllIncludingByAppointmentId_{appointmentId}";
        public static string GetAllIncludingByAppointmentIdForAdminAsync(int? appointmentId) => $"GetAllIncludingByAppointmentIdForAdmin_{appointmentId}";
        public static string GetByIdAsync(int? id) => $"GetAppointmentAnswerById_{id}";
    }
}
