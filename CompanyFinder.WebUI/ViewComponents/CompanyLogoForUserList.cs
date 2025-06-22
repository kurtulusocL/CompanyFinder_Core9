using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyLogoForUserList : ViewComponent
    {
        readonly ICompanyService _companyService;
        public CompanyLogoForUserList(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyLogo(id));
        }
    }
}
