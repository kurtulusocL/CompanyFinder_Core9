using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<AdTarget> AdTargets { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
