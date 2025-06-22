using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyPartnership : BaseEntity
    {
        public string Title { get; set; }
        public string? Detail { get; set; }
        public string Desc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public decimal? Price { get; set; }

        public int? CompanyId { get; set; }
        public int ProductCategoryId { get; set; }
        public string UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports  { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
    }
}
