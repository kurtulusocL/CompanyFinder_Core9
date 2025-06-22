using System.Net.Http;
using System.Text.Json;
using CompanyFinder.Core.Audits;
using CompanyFinder.Core.Helpers;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyFinder.Business.Attributes
{
    public class AuditLogAttribute : ActionFilterAttribute
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var scope = ServiceProviderHelper.ServiceProvider.CreateScope())
            {

                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var request = filterContext.HttpContext.Request;
                var userAgent = request.HttpContext.Request.Headers["User-Agent"].ToString();
                string macAddress = DeviceManufacturer.GetMACAddress();

                Audit audit = new Audit()
                {
                    UserName = (request.HttpContext.User.Identity.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                    UserId = request.HttpContext.Session.GetString("userId"),
                    IpAddress = IpAddress.FindUserIp(),
                    Browser = request.HttpContext.Request.Headers["User-Agent"].ToString(),
                    BrowserVersion = BrowserVersion.GetBrowserVersion(request.HttpContext.Request.Headers["User-Agent"].ToString()),
                    Language = request.HttpContext.Request.Headers["Accept-Language"].ToString(),
                    AreaAccessed = AreaAccessed.GetDetailedAreaAccessed(filterContext),
                    Host = request.HttpContext.Request.Host.ToString(),
                    ProxyConnection = request.HttpContext.Request.Headers["Connection"],
                    Device = DeviceType.GetDeviceType(userAgent),
                    DeviceModel = DeviceModel.GetDeviceModel(userAgent),
                    Platform = Platform.GetPlatform(userAgent),
                    Manufacturer = DeviceManufacturer.GetManufacturerFromMac(macAddress),
                    MacAddress = MacAddress.GetMacAddress(),
                    LocalIpAddress = LocalIpAddress.GetLocalIPAddress(),
                    RemoteIpAddress = RemoteIpAddress.GetRemoteIpAddress(request.HttpContext),
                    IpAddressVPN = IpAddressWithVpn.GetClientIPAddress(request.HttpContext),
                    CreatedDate = DateTime.Now
                };
                filterContext.HttpContext.Items["CurrentAudit"] = audit;

                dbContext.Audits.Add(audit);
                await SaveToFileAsJsonAsync(audit);
                await dbContext.SaveChangesAsync();
                base.OnActionExecuting(filterContext);
            }
        }
        private async Task SaveToFileAsJsonAsync(Audit audit)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AuditLogs.txt");
            var jsonString = JsonSerializer.Serialize(audit, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await _semaphore.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(filePath, jsonString + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Dosya yazma hatası: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }            
        }
    }
}
