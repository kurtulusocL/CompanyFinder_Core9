using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminLayoutNavbarUser : ViewComponent
    {
        readonly IUserService _userService;
        public AdminLayoutNavbarUser(IUserService userService)
        {
            _userService = userService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_userService.GetUserForAdminHomeById(id));
        }
    }
}
