using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserReportAnswer : ViewComponent
    {
        readonly IReportAnswerService _reportAnswerService;
        public CompanyUserReportAnswer(IReportAnswerService reportAnswerService)
        {
            _reportAnswerService = reportAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_reportAnswerService.GetAllIncludingReportAnswerForCompanyUserByReportId(id));
        }
    }
}
