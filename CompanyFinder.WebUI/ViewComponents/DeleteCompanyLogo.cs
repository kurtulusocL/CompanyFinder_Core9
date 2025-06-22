using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class DeleteCompanyLogo:ViewComponent
    {
        readonly ICompanyService _companyService;
        public DeleteCompanyLogo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyLogo(id));
        }
    }
}
