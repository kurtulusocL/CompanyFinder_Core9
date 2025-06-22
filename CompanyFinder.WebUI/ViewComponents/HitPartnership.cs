using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitPartnership:ViewComponent
    {
        readonly IHitService _hitService;
        public HitPartnership(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyPartnershipHit(id, userId, currentValue));
        }
    }
}
