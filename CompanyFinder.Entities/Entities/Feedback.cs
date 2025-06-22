using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Feedback : BaseEntity
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Desc { get; set; }

        public string UserId { get; set; }
    }
}
