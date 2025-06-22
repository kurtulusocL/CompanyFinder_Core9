using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class SavedContent : BaseEntity
    {
        public bool IsSaved { get; set; }
        public DateTime SavedDate { get; set; }

        public int? CompanyId { get; set; }
        public int? CompanyPartnershipId { get; set; }
        public int? ProductId { get; set; }
        public int? BlogId { get; set; }
        public string UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyPartnership CompanyPartnership { get; set; }
        public virtual Product Product { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }
    }
}
