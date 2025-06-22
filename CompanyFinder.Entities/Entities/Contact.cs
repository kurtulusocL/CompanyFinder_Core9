using System.ComponentModel.DataAnnotations;
using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Contact : BaseEntity
    {
        public string City { get; set; }
        public string Country { get; set; }

        [EmailAddress]
        public string PrincipalEmail { get; set; }

        [EmailAddress]
        public string BusinessEmail { get; set; }
        public string? Mernis { get; set; }
    }
}
