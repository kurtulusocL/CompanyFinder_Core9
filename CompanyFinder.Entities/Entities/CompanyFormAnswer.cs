using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyFormAnswer : BaseEntity
    {
        public string? Title { get; set; }
        public string Desc { get; set; }

        public int? CompanyFormId { get; set; }
        public string UserId { get; set; }

        public virtual CompanyForm CompanyForm { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
