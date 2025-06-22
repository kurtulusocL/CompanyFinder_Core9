using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class DropInformation : BaseEntity
    {       
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
