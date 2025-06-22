using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeAdCounter : ViewComponent
    {
        readonly IAdService _adService;
        public AdminHomeAdCounter(IAdService adService)
        {
            _adService = adService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var adCount = _adService.AdCounter();
                ViewData["adCount"] = adCount >= 0 ? adCount : 0;
            }
            catch (Exception)
            {
                ViewData["adCount"] = 0;
            }
            return View();
        }
    }
}
