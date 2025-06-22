using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerFormRandom:ViewComponent
    {
        readonly ICompanyFormService _companyFormService;
        public ExplorerFormRandom(ICompanyFormService companyFormService)
        {
            _companyFormService = companyFormService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyFormService.GetAllIncludingRandom());
        }
    }
}
