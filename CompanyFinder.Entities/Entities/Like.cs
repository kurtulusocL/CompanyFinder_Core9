using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Like : BaseEntity
    {
        public int? Value { get; set; } = 0;

        public int? BlogId { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyPartnershipId { get; set; }
        public int? CompanyFormId { get; set; }
        public int? ProductId { get; set; }
        public int? SectorNewsId { get; set; }
        public string UserId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyPartnership CompanyPartnership { get; set; }
        public virtual CompanyForm CompanyForm { get; set; }
        public virtual Product Product { get; set; }
        public virtual SectorNews SectorNews { get; set; }
        public virtual User User { get; set; }
    }
}
