using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities.AppUser
{
    public class UserSession : BaseEntity
    {
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsOnline { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}