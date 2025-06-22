using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CustomerStatus:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
