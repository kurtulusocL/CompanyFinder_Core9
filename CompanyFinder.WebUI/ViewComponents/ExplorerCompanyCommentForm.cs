using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyCommentForm:ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyCommentForm(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.CompanyId = _companyService.GetCompanyById(id);
            Comment model = new Comment
            {
                CompanyId = id
            };
            return View(model);
        }
    }
}
