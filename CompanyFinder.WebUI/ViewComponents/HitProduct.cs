using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitProduct:ViewComponent
    {
        readonly IHitService _hitService;
        public HitProduct(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.ProductHit(id, userId, currentValue));
        }
    }
}
