using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AboutCityCounter : ViewComponent
    {
        readonly ICityService _cityService;
        public AboutCityCounter(ICityService cityService)
        {
            _cityService = cityService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var cityCount = _cityService.CityCounter();
                ViewData["cityCount"] = cityCount >= 0 ? cityCount : 0;
            }
            catch (Exception)
            {
                ViewData["cityCount"] = 0;
            }
            return View();
        }
    }
}
