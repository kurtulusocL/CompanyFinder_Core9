using System.Net.Sockets;
using System.Net;

namespace CompanyFinder.Core.Audits
{
    public static class LocalIpAddress
    {
        public static string GetLocalIPAddress()
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
                return "No IPv4 Found";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Local IP alınırken hata: {ex.Message}");
                return "Unknown";
            }
        }
    }
}
