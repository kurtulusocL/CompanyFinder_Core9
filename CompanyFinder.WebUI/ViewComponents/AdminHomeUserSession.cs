using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeUserSession:ViewComponent
    {
        readonly IUserSessionService _userSessionService;
        public AdminHomeUserSession(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_userSessionService.GetAllUserSessionForAdminHome());
        }
    }
}
