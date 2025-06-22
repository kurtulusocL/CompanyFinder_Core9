using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class GetCompanyLogo:ViewComponent
    {
        readonly ICompanyService _companyService;
        public GetCompanyLogo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyLogoForCommentByUserId(id));
        }
    }
}
