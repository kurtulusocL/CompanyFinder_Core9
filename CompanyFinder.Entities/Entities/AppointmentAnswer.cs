using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class AppointmentAnswer : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime ReAppointmentDate { get; set; }
        public bool ReAppointmentApproved { get; set; } = false;
        public DateTime? ReApprovedDate { get; set; }

        public int? AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
