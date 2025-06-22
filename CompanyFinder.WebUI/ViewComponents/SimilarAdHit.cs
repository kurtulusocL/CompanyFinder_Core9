using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class SimilarAdHit : ViewComponent
    {
        readonly IAdService _adService;
        public SimilarAdHit(IAdService adService)
        {
            _adService = adService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_adService.SimilarHitRead(id));
        }
    }
}