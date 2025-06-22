using CompanyFinder.Core.Entities.EntityFramework;

namespace CompanyFinder.Entities.Entities
{
    public class Blocked : BaseEntity
    {
        public string? MacAddress { get; set; }
        public string? IpAddress { get; set; }
        public string? LocalIpAddress { get; set; }
        public string? RemoteIpAddress { get; set; }
        public string? IpAddressVPN { get; set; }
        public string? Host { get; set; }
        public string? Note { get; set; }
    }
}