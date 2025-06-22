using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class ReportAnswer : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool? IsSatisfield { get; set; }

        public int? ReportId { get; set; }
        public virtual Report Report { get; set; }
    }
}
