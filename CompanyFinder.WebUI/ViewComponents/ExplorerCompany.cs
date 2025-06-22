using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompany : ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompany(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyService.GetAllIncludingCompaniesForExplorer());
        }
    }
}
