using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CompanySubcategory : BaseEntity
    {
        public string Name { get; set; }

        public int? CompanyCategoryId { get; set; }
        public virtual CompanyCategory CompanyCategory { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
