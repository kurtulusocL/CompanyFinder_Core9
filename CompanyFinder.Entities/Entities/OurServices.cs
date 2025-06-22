using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class OurServices : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string? IconUrl { get; set; }
    }
}
