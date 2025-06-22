using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyMessage : BaseEntity
    {
        public string? Title { get; set; }
        public string Subject { get; set; }
        public string Desc { get; set; }

        public int? CompanyId { get; set; }
        public string UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
