using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Appointment : BaseEntity
    {
        public string Subject { get; set; }
        public string Desc { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovedDate { get; set; }

        public int? CompanyId { get; set; }
        public string UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<AppointmentAnswer> AppointmentAnswers { get; set; }
    }
}