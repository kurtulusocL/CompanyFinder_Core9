using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class CancelMembership : BaseEntity
    {
        public string Title { get; set; }
        public string? AnotherReason { get; set; }
        public DateTime ExpectedCancelDate { get; set; }
        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelDate { get; set; }

        public int CancelMembershipReasonId { get; set; }
        public string UserId { get; set; }

        public virtual CancelMembershipReason CancelMembershipReason { get; set; }
        public virtual User User { get; set; }
    }
}
