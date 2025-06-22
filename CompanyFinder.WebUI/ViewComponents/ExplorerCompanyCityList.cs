using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyCityList:ViewComponent
    {
        readonly ICityService _cityService;
        public ExplorerCompanyCityList(ICityService cityService)
        {
            _cityService = cityService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_cityService.GetAllIncludingCitiesForCompanies());
        }
    }
}
