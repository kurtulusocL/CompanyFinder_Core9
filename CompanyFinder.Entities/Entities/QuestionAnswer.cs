using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class QuestionAnswer : BaseEntity
    {
        public string? Title{ get; set; }
        public string Answer { get; set; }

        public int? QuestionId { get; set; }
        public string UserId { get; set; }

        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}