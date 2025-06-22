using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserCompanyPartnershipContactInfo:ViewComponent
    {
        readonly ICompanyContactService _companyContactService;
        public CompanyUserCompanyPartnershipContactInfo(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_companyContactService.GetCompanyContactForcCompanyByCompanyId(id));
        }
    }
}
