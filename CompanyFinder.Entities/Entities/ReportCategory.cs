using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class ReportCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
