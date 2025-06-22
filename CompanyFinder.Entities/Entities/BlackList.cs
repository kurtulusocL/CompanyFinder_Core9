using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class BlackList : BaseEntity
    {
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string RemoteIpAddress { get; set; }
        public string IpAddressVPN { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int? AuditId { get; set; }
        public virtual Audit Audit { get; set; }
    }
}
