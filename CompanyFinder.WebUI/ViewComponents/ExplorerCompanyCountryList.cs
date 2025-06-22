using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyCountryList:ViewComponent
    {
        readonly ICountryService _countryService;
        public ExplorerCompanyCountryList(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_countryService.GetAllIncludingCountriesForCompany());
        }
    }
}
