using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeReportCounter:ViewComponent
    {
        readonly IReportService _reportService;
        public AdminHomeReportCounter(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var reportCount = _reportService.ReportCounter();
                ViewData["reportCount"] = reportCount >= 0 ? reportCount : 0;
            }
            catch (Exception)
            {
                ViewData["reportCount"] = 0;
            }
            return View();
        }
    }
}
