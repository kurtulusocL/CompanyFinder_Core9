using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Desc { get; set; }
        public int Hit { get; set; } = 0;
        public int Like { get; set; } = 0;
        public string ImageUrl { get; set; }        
    }
}
