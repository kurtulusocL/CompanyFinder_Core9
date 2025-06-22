using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeCompany : ViewComponent
    {
        readonly ICompanyService _companyService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyUserHomeCompany(ICompanyService companyService, IHttpContextAccessor httpContextAccessor)
        {
            _companyService = companyService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IViewComponentResult Invoke(string id)
        {
            ViewData["userId"] = _httpContextAccessor.HttpContext.Session.GetString("userId");
            return View(_companyService.GetIncludingCompaniesByUserId(id));
        }
    }
}
