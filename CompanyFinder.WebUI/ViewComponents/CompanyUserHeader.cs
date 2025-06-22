using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHeader : ViewComponent
    {
        readonly IUserService _userService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyUserHeader(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke(string id)
        {
            id = _httpContextAccessor.HttpContext.Session.GetString("userId");
            if (id != null)
            {
                return View(_userService.GetUserCompanyHeaderByUserId(id));
            }
            return View();
        }
    }
}
