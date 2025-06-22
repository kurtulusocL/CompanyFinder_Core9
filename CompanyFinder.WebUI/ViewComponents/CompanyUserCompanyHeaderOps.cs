using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserCompanyHeaderOps:ViewComponent
    {
        readonly ICompanyService _companyService;
        public CompanyUserCompanyHeaderOps(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetIncludingCompanyForHeaderByUserId(id));
        }
    }
}
