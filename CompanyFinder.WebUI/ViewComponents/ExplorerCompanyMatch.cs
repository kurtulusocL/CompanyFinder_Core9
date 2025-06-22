using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyMatch : ViewComponent
    {
        readonly ICompanyCategoryService _companyCategoryService;
        readonly ICompanySubcategoryService _companySubcategoryService;
        readonly ICityService _cityService;
        readonly ICountryService _countryService;
        public ExplorerCompanyMatch(ICompanyCategoryService companyCategoryService, ICompanySubcategoryService companySubcategoryService, ICityService cityService, ICountryService countryService)
        {
            _companyCategoryService = companyCategoryService;
            _companySubcategoryService = companySubcategoryService;
            _cityService = cityService;
            _countryService = countryService;
        }
        public IViewComponentResult Invoke(string key)
        {
            ViewBag.CompanyCategories = _companyCategoryService.GetAllIncludingCompanyCategoriesForCompanyAdvancedSearch();
            ViewBag.CompanySubcategories = _companySubcategoryService.GetAllIncludingCompanySubcategoriesForCompanyAdvancedSearch();
            ViewBag.CompanyCities = _cityService.GetAllIncludingCitiesForCompanyAdvancedSearch();
            ViewBag.CompanyCountries = _countryService.GetAllIncludingCountriesForCompanyAdvancedSearch();
            return View();
        }
    }
}