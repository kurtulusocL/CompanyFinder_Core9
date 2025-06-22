using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitAd:ViewComponent
    {
        readonly IHitService _hitService;
        public HitAd(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.AdHit(id, userId, currentValue));
        }
    }
}
