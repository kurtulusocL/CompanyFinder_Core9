using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CancelMembershipReason : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<CancelMembership> CancelMemberships { get; set; }
    }
}
