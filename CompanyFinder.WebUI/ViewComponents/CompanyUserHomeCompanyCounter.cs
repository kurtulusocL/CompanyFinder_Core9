using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeCompanyCounter : ViewComponent
    {
        readonly ICompanyService _companyService;
        public CompanyUserHomeCompanyCounter(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetIncludingCompanyForCompanyUserCounter(id));
        }
    }
}
