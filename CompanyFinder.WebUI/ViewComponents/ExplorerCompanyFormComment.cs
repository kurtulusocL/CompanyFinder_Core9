using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyFormComment : ViewComponent
    {
        readonly ICompanyFormAnswerService _companyFormAnswerService;
        public ExplorerCompanyFormComment(ICompanyFormAnswerService companyFormAnswerService)
        {
            _companyFormAnswerService = companyFormAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_companyFormAnswerService.GetAllIncludingCompanyFormAnswersForExplorer(id));
        }
    }
}
