using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public bool ForCompany { get; set; }
        public bool ForAdmin { get; set; }
        public bool ForEverybody { get; set; }
        public string? ImageUrl { get; set; }
        public int Hit { get; set; } = 0;
    }
}
