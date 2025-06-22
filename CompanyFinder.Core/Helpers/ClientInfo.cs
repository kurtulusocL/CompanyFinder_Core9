using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Core.Helpers
{
    public class ClientInfo
    {
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string LocalIpAddress { get; set; }
        public string RemoteIpAddress { get; set; }
        public string IpAddressVPN { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
    }
}
