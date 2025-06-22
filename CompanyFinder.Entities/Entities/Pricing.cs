using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Pricing : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }

        public int PricingCategoryId { get; set; }
        public virtual PricingCategory PricingCategory { get; set; }

        public virtual ICollection<PricingContact> PricingContacts { get; set; }
    }
}
