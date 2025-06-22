using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeReportAnswerCounter:ViewComponent
    {
        readonly IReportAnswerService _reportAnswerService;
        public AdminHomeReportAnswerCounter(IReportAnswerService reportAnswerService)
        {
            _reportAnswerService = reportAnswerService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var reportAnswerCount = _reportAnswerService.ReportAnswerCounter();
                ViewData["reportAnswerCount"] = reportAnswerCount >= 0 ? reportAnswerCount : 0;
            }
            catch (Exception)
            {
                ViewData["reportAnswerCount"] = 0;
            }
            return View();
        }
    }
}
