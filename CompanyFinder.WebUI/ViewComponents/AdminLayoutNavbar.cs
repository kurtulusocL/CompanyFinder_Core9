using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminLayoutNavbar:ViewComponent
    {
        readonly IUserService _userService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public AdminLayoutNavbar(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke(string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("adminId");
            if (userId != null)
            {
                return View(_userService.GetUserForAdminHomeById(userId));
            }
            return View();
        }
    }
}
