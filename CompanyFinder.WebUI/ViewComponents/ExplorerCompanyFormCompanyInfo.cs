using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyFormCompanyInfo:ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyFormCompanyInfo(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetCompanyInformationForCommentByUserId(id));
        }
    }
}
