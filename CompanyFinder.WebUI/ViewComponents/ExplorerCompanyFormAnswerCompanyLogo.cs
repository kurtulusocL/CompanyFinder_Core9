using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyFormAnswerCompanyLogo : ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyFormAnswerCompanyLogo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyLogoForCommentByUserId(id));
        }
    }
}
