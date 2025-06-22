using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeAbout : ViewComponent
    {
        readonly IAboutService _aboutService;
        public HomeAbout(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_aboutService.GetAllAboutForHome());
        }
    }
}
