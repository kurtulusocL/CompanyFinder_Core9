using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeFooterAbout:ViewComponent
    {
        readonly IAboutService _aboutService;
        public HomeFooterAbout(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_aboutService.GetAllAboutForFooter());
        }
    }
}
