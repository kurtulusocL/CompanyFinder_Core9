using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class IconLogo:ViewComponent
    {
        readonly ILogoService _logoService;
        public IconLogo(ILogoService logoService)
        {
            _logoService = logoService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_logoService.GetAllLogoForIcon());
        }
    }
}
