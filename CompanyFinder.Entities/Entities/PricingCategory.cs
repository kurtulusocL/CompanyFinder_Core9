using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class PricingCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Pricing> Pricings { get; set; }
    }
}
