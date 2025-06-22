using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserBlogReport:ViewComponent
    {
        readonly IReportService _reportService;
        public CompanyUserBlogReport(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_reportService.GetAllIncludingBlogReportForCompanyUserByBlogId(id));
        }
    }
}
