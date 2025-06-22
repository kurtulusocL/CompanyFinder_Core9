using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class RandomAdForVisitor:ViewComponent
    {
        readonly IAdService _adService;
        public RandomAdForVisitor(IAdService adService)
        {
            _adService = adService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_adService.GetAllIncludingRandomAd());
        }
    }
}
