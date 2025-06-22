using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<AdTarget> AdTargets { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
