using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Logo : BaseEntity
    {
        public string? Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
