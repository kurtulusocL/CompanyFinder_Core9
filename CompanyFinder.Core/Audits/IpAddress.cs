using System.Net.Sockets;
using System.Net;

namespace CompanyFinder.Core.Audits
{
    public static class IpAddress
    {
        public static string FindUserIp()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "No Network Found";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User IP bulunurken hata: {ex.Message}");
                return "Unknown";
            }
        }
    }
}
