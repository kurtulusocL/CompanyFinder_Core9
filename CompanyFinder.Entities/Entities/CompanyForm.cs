using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class CompanyForm : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAnswerable { get; set; }

        public int FormCategoryId { get; set; }
        public int? CompanyId { get; set; }

        public virtual FormCategory FormCategory { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<CompanyFormAnswer> CompanyFormAnswers { get; set; }
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
