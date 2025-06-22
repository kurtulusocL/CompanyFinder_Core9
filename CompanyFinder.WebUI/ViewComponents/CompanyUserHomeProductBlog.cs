using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeProductBlog:ViewComponent
    {
        readonly ICompanyService _companyService;
        public CompanyUserHomeProductBlog(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_companyService.GetIncludingCompaniesByUserId(id));
        }
    }
}
