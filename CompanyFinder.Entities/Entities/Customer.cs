using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Customer : BaseEntity
    {
        public string NameSurname { get; set; }
        public string CustomerCompany { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string Location { get; set; }
        public string? Desc { get; set; }

        public int? CompanyId { get; set; }
        public int CustomerStatusId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CustomerStatus CustomerStatus { get; set; }
    }
}