using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeLocation:ViewComponent
    {
        readonly ICityService _cityService;
        public AdminHomeLocation(ICityService cityService)
        {
            _cityService = cityService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cityService.GetAllCityForAdminHome());
        }
    }
}
