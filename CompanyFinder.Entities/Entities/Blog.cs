using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }

        public int BlogCategoryId { get; set; }
        public int? CompanyId { get; set; }
        public string UserId { get; set; }

        public virtual BlogCategory BlogCategory { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SaveContents { get; set; }
    }
}
