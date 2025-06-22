using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyForm:ViewComponent
    {
        readonly ICompanyFormService _companyFormService;
        public ExplorerCompanyForm(ICompanyFormService companyFormService)
        {
            _companyFormService = companyFormService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyFormService.GetAllIncludingCompanyFormRandomForExplorerHome());
        }
    }
}
