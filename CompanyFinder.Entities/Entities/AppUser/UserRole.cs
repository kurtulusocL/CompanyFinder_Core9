using CompanyFinder.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CompanyFinder.Entities.Entities.AppUser
{
    public class UserRole : IdentityRole, IEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? SuspendedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
