using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class NavbarHomeAccount : ViewComponent
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public NavbarHomeAccount(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["userId"] = _httpContextAccessor.HttpContext.Session.GetString("userId");
            ViewData["adminId"] = _httpContextAccessor.HttpContext.Session.GetString("adminId");
            return View();
        }
    }
}
