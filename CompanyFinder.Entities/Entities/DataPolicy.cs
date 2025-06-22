using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class DataPolicy : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}
