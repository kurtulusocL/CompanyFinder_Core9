using System.ComponentModel.DataAnnotations;

namespace CompanyFinder.Core.Entities.EntityFramework
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? SuspendedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
