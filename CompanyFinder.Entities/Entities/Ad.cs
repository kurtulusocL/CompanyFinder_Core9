using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Ad : BaseEntity
    {
        public string Text { get; set; }
        public string? Subtext { get; set; }
        public DateTime ShowDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool HasTarget { get; set; }
        public string? ImageUrl { get; set; }
        public string? RedirectWebUrl { get; set; }
        public int? SimilarHit { get; set; } = 0;

        public int AdCompanyId { get; set; }
        public virtual AdCompany AdCompany { get; set; }

        public virtual ICollection<AdTarget> AdTargets { get; set; }
        public virtual ICollection<Hit> Hits  { get; set; }
    }
}
