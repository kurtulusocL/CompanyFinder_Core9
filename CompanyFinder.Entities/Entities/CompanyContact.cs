using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyContact : BaseEntity
    {
        [EmailAddress, AllowNull]
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WhatsappNo { get; set; }
        public string? Address { get; set; }
        public bool IsHide { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
