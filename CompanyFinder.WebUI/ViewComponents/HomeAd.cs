using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeAd:ViewComponent
    {
        readonly IAdService _adService;
        public HomeAd(IAdService adService)
        {
            _adService = adService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_adService.GetAllIncludingRandomAdForHome());
        }
    }
}
