using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<CompanyPartnership> CompanyPartnerships{ get; set; }
        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
