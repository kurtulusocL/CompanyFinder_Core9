using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class PricingContact : BaseEntity
    {
        public string NameSurname { get; set; }
        public string CompanyName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Desc { get; set; }

        public int? PricingId { get; set; }
        public virtual Pricing Pricing { get; set; }
    }
}