using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeHeaderLastReports : ViewComponent
    {
        readonly IReportService _reportService;
        public AdminHomeHeaderLastReports(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_reportService.GetAllIncludingReportsForAdminHeader());
        }
    }
}
