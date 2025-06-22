using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeCompanyCounter:ViewComponent
    {
        readonly ICompanyService _companyService;
        public HomeCompanyCounter(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var companyCount = _companyService.CompanyCounter();
                ViewData["companyCount"] = companyCount >= 0 ? companyCount : 0;
            }
            catch (Exception)
            {
                ViewData["companyCount"] = 0;
            }
            return View();
        }
    }
}
