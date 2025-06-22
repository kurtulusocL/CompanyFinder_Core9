using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserRandomPartnership:ViewComponent
    {
        readonly ICompanyPartnershipService _companyPartnershipService;
        public CompanyUserRandomPartnership(ICompanyPartnershipService companyPartnershipService)
        {
            _companyPartnershipService = companyPartnershipService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyPartnershipService.GetAllIncludingCompanyPartnershipRandom());
        }
    }
}
