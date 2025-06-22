using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class ProductSubcategory : BaseEntity
    {
        public string Name { get; set; }

        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
