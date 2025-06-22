using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class SectorNews : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string? Detail { get; set; }
        public string? RedirectUrl { get; set; }
        public string? Source { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}