using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerLastCompany : ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerLastCompany(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyService.GetAllIncludingLastCompaniesForExplorer());
        }
    }
}
