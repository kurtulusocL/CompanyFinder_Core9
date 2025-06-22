using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeCompanyLogo:ViewComponent
    {
        readonly ILogoService _logoService;
        public HomeCompanyLogo(ILogoService logoService)
        {
            _logoService = logoService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_logoService.GetAllLogoForHome());
        }
    }
}
