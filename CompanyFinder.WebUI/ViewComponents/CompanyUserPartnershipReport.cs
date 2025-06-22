using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserPartnershipReport:ViewComponent
    {
        readonly IReportService _reportService;
        public CompanyUserPartnershipReport(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_reportService.GetAllIncludingPartnershipReportForCompanyUserByCompanyPartnershipId(id));
        }
    }
}
