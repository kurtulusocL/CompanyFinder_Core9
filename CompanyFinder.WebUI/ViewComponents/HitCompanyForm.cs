using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HitCompanyForm : ViewComponent
    {
        readonly IHitService _hitService;
        public HitCompanyForm(IHitService hitService)
        {
            _hitService = hitService;
        }
        public IViewComponentResult Invoke(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyFormHit(id, userId, currentValue));
        }
    }
}
