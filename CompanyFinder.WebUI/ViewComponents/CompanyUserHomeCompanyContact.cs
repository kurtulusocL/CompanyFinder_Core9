using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeCompanyContact:ViewComponent
    {
        readonly ICompanyContactService _companyContactService;
        public CompanyUserHomeCompanyContact(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_companyContactService.GetCompanyContactForcCompanyByCompanyId(id));
        }
    }
}
