using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
