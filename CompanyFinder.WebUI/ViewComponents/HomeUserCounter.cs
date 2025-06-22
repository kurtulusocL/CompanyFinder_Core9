using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeUserCounter:ViewComponent
    {
        readonly IUserService _userService;
        public HomeUserCounter(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var userCount = _userService.UserCounter();
                ViewData["userCount"] = userCount >= 0 ? userCount : 0;
            }
            catch (Exception)
            {
                ViewData["userCount"] = 0;
            }
            return View();
        }
    }
}
