using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Stock : BaseEntity
    {
        public int Code { get; set; }
        public int Quantity { get; set; }
        public string? Warehouse { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
