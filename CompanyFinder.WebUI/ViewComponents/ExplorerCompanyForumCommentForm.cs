using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyForumCommentForm:ViewComponent
    {
        readonly ICompanyFormService _companyFormService;
        public ExplorerCompanyForumCommentForm(ICompanyFormService companyFormService)
        {
            _companyFormService = companyFormService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.CompanyFormId = _companyFormService.GetByIdAsync(id);
            CompanyFormAnswer model = new CompanyFormAnswer
            {
                CompanyFormId = id
            };
            return View(model);
        }
    }
}
