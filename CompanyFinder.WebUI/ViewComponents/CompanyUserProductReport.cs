using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProductReport:ViewComponent
    {
        readonly IReportService _reportService;
        public CompanyUserProductReport(IReportService reportService)
        {
            _reportService = reportService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_reportService.GetAllIncludingProductReportForCompanyUserByProductId(id));
        }
    }
}
