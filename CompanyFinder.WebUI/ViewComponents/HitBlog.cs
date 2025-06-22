using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitBlog:ViewComponent
    {
        readonly IHitService _hitService;
        public HitBlog(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.BlogHit(id, userId, currentValue));
        }
    }
}
