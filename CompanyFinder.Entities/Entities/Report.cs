using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Report : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int? Hit { get; set; } = 0;
        public bool IsFixed { get; set; } = false;
        public DateTime? FixedDate { get; set; }

        public int ReportCategoryId { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
        public int? CommentAnswerId { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyPartnershipId { get; set; }
        public int? CompanyFormId { get; set; }
        public int? CompanyFormAnswerId { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }

        public virtual ReportCategory ReportCategory { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual CommentAnswer CommentAnswer { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyPartnership CompanyPartnership { get; set; }
        public virtual CompanyForm CompanyForm { get; set; }
        public virtual CompanyFormAnswer CompanyFormAnswer { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ReportAnswer> ReportAnswers { get; set; }
    }
}
