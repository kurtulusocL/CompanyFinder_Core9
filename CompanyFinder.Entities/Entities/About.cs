using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public int Hit { get; set; } = 0;
    }
}
