using System.Collections.Concurrent;
using CompanyFinder.Core.Helpers;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.Business.Constants.Middlewares
{
    public class BlockUserMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ConcurrentDictionary<string, DateTime> _blockedCache = new();
        private static DateTime _lastCacheUpdate = DateTime.MinValue;
        private static int _updateInProgress = 0;
        private const int CACHE_REFRESH_MINUTES = 1;

        public BlockUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
        {
            var host = context.Request.Host.Value?.ToLower();           
            if (host.Contains("localhost") && host.Contains("127.0.0.1") && host == ":1")
            {
                return;
            }

            await UpdateCacheIfNeeded(dbContext);

            if (_blockedCache.IsEmpty)
            {
                await _next(context);
                return;
            }

            var clientInfo = GetClientInfo(context);
            if (clientInfo == null)
            {
                await _next(context);
                return;
            }

            var isBlocked = IsBlockedInMemory(clientInfo);

            if (isBlocked)
            {
                await SendBlockedResponse(context);
                return;
            }

            await _next(context);
        }

        private async Task UpdateCacheIfNeeded(ApplicationDbContext dbContext)
        {
            if (DateTime.Now.Subtract(_lastCacheUpdate).TotalMinutes < CACHE_REFRESH_MINUTES)
                return;

            if (Interlocked.CompareExchange(ref _updateInProgress, 1, 0) != 0)
                return;

            try
            {
                if (DateTime.Now.Subtract(_lastCacheUpdate).TotalMinutes < CACHE_REFRESH_MINUTES)
                    return;

                var blockedItems = await dbContext.Blockeds
                    .Where(i => i.IsActive && !i.IsDeleted)
                    .Select(b => new
                    {
                        b.MacAddress,
                        b.IpAddress,
                        b.LocalIpAddress,
                        b.RemoteIpAddress,
                        b.IpAddressVPN,
                        b.Host
                    })
                    .AsNoTracking()
                    .ToListAsync();

                var newCache = new ConcurrentDictionary<string, DateTime>();

                foreach (var item in blockedItems)
                {
                    if (!string.IsNullOrEmpty(item.MacAddress))
                        newCache.TryAdd($"MAC:{item.MacAddress.Trim().ToLower()}", DateTime.Now);

                    if (!string.IsNullOrEmpty(item.IpAddress))
                        newCache.TryAdd($"IP:{item.IpAddress.Trim()}", DateTime.Now);

                    if (!string.IsNullOrEmpty(item.LocalIpAddress))
                        newCache.TryAdd($"LOCAL:{item.LocalIpAddress.Trim()}", DateTime.Now);

                    if (!string.IsNullOrEmpty(item.RemoteIpAddress))
                        newCache.TryAdd($"REMOTE:{item.RemoteIpAddress.Trim()}", DateTime.Now);

                    if (!string.IsNullOrEmpty(item.IpAddressVPN))
                        newCache.TryAdd($"VPN:{item.IpAddressVPN.Trim()}", DateTime.Now);

                    if (!string.IsNullOrEmpty(item.Host))
                        newCache.TryAdd($"HOST:{item.Host.Trim().ToLower()}", DateTime.Now);
                }
                _blockedCache.Clear();
                foreach (var item in newCache)
                {
                    _blockedCache.TryAdd(item.Key, item.Value);
                }

                _lastCacheUpdate = DateTime.Now;
            }
            finally
            {
                Interlocked.Exchange(ref _updateInProgress, 0);
            }
        }

        private bool IsBlockedInMemory(ClientInfo clientInfo)
        {
            if(!string.IsNullOrEmpty(clientInfo.MacAddress) &&
                _blockedCache.ContainsKey($"MAC:{clientInfo.MacAddress.Trim().ToLower()}"))
                return true;

            if (!string.IsNullOrEmpty(clientInfo.IpAddress) &&
                _blockedCache.ContainsKey($"IP:{clientInfo.IpAddress.Trim()}"))
                return true;

            if (!string.IsNullOrEmpty(clientInfo.LocalIpAddress) &&
                _blockedCache.ContainsKey($"LOCAL:{clientInfo.LocalIpAddress.Trim()}"))
                return true;

            if (!string.IsNullOrEmpty(clientInfo.RemoteIpAddress) &&
                _blockedCache.ContainsKey($"REMOTE:{clientInfo.RemoteIpAddress.Trim()}"))
                return true;

            if (!string.IsNullOrEmpty(clientInfo.IpAddressVPN) &&
                _blockedCache.ContainsKey($"VPN:{clientInfo.IpAddressVPN.Trim()}"))
                return true;

            if (!string.IsNullOrEmpty(clientInfo.Host) &&
                _blockedCache.ContainsKey($"HOST:{clientInfo.Host.Trim().ToLower()}"))
                return true;

            return false;
        }

        private async Task SendBlockedResponse(HttpContext context)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync("Bu siteye erişiminiz engellenmiştir.");
            await context.Response.CompleteAsync();
        }

        private ClientInfo GetClientInfo(HttpContext context)
        {
            var clientIp = GetClientIpAddress(context);
            if (string.IsNullOrEmpty(clientIp))
                return null;

            return new ClientInfo
            {
                IpAddress = clientIp,
                MacAddress = GetMacAddress(context),
                LocalIpAddress = GetLocalIpAddress(context),
                RemoteIpAddress = GetRemoteIpAddress(context),
                IpAddressVPN = GetVpnIpAddress(context),
                Host = context.Request.Host.Value,
                UserAgent = context.Request.Headers["User-Agent"].ToString()
            };
        }

        private string GetMacAddress(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Mac-Address", out var macHeader))
                return macHeader.ToString();

            if (context.Request.Headers.TryGetValue("Mac-Address", out var macHeader2))
                return macHeader2.ToString();

            if (context.Request.HasFormContentType && context.Request.Form.ContainsKey("macAddress"))
                return context.Request.Form["macAddress"].ToString();

            return null;
        }

        private string GetClientIpAddress(HttpContext context)
        {
            string clientIp = null;
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedForHeader))
            {
                clientIp = forwardedForHeader.ToString().Split(',').FirstOrDefault()?.Trim();
            }

            if (string.IsNullOrEmpty(clientIp) && context.Connection.RemoteIpAddress != null)
            {
                clientIp = context.Connection.RemoteIpAddress.ToString();
            }

            if (clientIp == "::1")
            {
                clientIp = "127.0.0.1";
            }

            if (clientIp?.StartsWith("::ffff:") == true)
            {
                clientIp = clientIp.Replace("::ffff:", "");
            }

            return clientIp?.Trim();
        }

        private string GetLocalIpAddress(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Local-IP", out var localIpHeader))
                return localIpHeader.ToString();

            if (context.Request.Headers.TryGetValue("Local-IP-Address", out var localIpHeader2))
                return localIpHeader2.ToString();

            if (context.Request.HasFormContentType && context.Request.Form.ContainsKey("localIpAddress"))
                return context.Request.Form["localIpAddress"].ToString();

            return null;
        }

        private string GetRemoteIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString();
        }

        private string GetVpnIpAddress(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-VPN-IP", out var vpnIpHeader))
                return vpnIpHeader.ToString();

            if (context.Request.Headers.TryGetValue("VPN-IP-Address", out var vpnIpHeader2))
                return vpnIpHeader2.ToString();

            if (context.Request.HasFormContentType && context.Request.Form.ContainsKey("vpnIpAddress"))
                return context.Request.Form["vpnIpAddress"].ToString();

            return null;
        }
    }
}