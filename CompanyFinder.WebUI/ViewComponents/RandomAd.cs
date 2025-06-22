using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class RandomAd:ViewComponent
    {
        readonly IAdService _adService;
        public RandomAd(IAdService adService)
        {
            _adService = adService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_adService.GetAllIncludingRandomAd());
        }
    }
}
