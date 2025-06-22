using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyDetailQuestion:ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyDetailQuestion(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.CompanyId = _companyService.GetCompanyById(id);
            Question model = new Question
            {
                CompanyId = id
            };
            return View(model);
        }
    }
}
