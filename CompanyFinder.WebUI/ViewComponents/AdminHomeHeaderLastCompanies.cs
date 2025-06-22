using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeHeaderLastCompanies : ViewComponent
    {
        readonly ICompanyService _companyService;
        public AdminHomeHeaderLastCompanies(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyService.GetAllIncludingLastCompaniesForAdminHeader());
        }
    }
}
