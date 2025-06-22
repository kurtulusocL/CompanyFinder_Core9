using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminLayoutMobileNavbar : ViewComponent
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public AdminLayoutMobileNavbar(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["adminId"] = _httpContextAccessor.HttpContext.Session.GetString("adminId");
            return View();
        }
    }
}
