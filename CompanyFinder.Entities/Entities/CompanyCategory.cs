using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanySubcategory> CompanySubcategories { get; set; }
    }
}
