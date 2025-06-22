using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Frequently:BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}
