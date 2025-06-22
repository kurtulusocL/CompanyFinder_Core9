using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeLastCompanies:ViewComponent
    {
        readonly ICompanyService _companyService;
        public AdminHomeLastCompanies(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyService.GetAllIncludingLastCompaniesForAdminHome());
        }
    }
}
