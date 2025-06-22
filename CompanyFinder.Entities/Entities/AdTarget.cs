using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class AdTarget : BaseEntity
    {
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? AdId { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
