using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Comment : BaseEntity
    {
        public string? Subject { get; set; }
        public string Text { get; set; }

        public int? BlogId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Company Company { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<CommentAnswer> CommentAnswers { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
