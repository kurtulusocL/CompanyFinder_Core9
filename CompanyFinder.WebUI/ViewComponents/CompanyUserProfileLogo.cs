using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProfileLogo:ViewComponent
    {
        readonly ICompanyService _companyService;
        public CompanyUserProfileLogo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetIncludeCompanyForCompanyUserLogoByUserId(id));
        }
    }
}
