using CompanyFinder.Core.Entities.EntityFramework;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Entities.Entities
{
    public class ProfileImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
