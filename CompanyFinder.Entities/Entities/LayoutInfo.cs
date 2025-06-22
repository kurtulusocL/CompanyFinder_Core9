using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class LayoutInfo:BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keyword { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public string Icon { get; set; }
    }
}
