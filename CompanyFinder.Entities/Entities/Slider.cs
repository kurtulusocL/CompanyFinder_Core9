using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Slider : BaseEntity
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
