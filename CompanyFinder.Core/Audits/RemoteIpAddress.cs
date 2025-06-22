using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Core.Audits
{
    public static class RemoteIpAddress
    {
        public static string GetRemoteIpAddress(HttpContext context)
        {
            try
            {
                var forwardedHeader = context.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(forwardedHeader))
                {
                    return forwardedHeader.Split(',')[0].Trim();
                }

                var remoteIp = context.Connection.RemoteIpAddress;
                if (remoteIp == null)
                    return "Unknown";

                if (remoteIp.IsIPv4MappedToIPv6)
                    return remoteIp.MapToIPv4().ToString();

                return remoteIp.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remote IP alınırken hata: {ex.Message}");
                return "Unknown";
            }
        }
    }
}
