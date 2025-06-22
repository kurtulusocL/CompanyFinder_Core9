using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public int Hit { get; set; } = 0;

        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
