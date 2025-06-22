using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class CommentAnswer : BaseEntity
    {
        public string? Title { get; set; }
        public string Text { get; set; }

        public int? CommentId { get; set; }
        public string UserId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
        
        public virtual ICollection<Report> Reports { get; set; }
    }
}
