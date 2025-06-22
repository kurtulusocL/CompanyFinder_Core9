using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class ToDo : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFinished { get; set; } = false;
        public DateTime? FinishedDate { get; set; }
        public int Hit { get; set; } = 0;

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
