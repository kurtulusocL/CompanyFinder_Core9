using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHeaderUser:ViewComponent
    {
        readonly IUserService _userService;
        public CompanyUserHeaderUser(IUserService userService)
        {
            _userService = userService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_userService.GetUserCompanyHeaderByUserId(id));
        }
    }
}
