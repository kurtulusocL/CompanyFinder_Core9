using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class FormCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<CompanyForm> CompanyForms { get; set; }
    }
}
