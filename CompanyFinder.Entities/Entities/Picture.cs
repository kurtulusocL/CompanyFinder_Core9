using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Picture : BaseEntity
    {
        public string ImageUrl { get; set; }

        public int? BlogId { get; set; }
        public int? ProductId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Product Product { get; set; }
        public virtual Company Company { get; set; }
    }
}
