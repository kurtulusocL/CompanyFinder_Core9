using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyRandomCompany : ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyRandomCompany(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyService.GetAllIncludingRandomCompanies());
        }
    }
}
