using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCommentCompanyLogo:ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCommentCompanyLogo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyLogoForCommentByUserId(id));
        }
    }
}
