using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitCompany : ViewComponent
    {
        readonly IHitService _hitService;
        public HitCompany(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyHit(id, userId, currentValue));
        }
    }
}
