using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeQuestionAnswerCounter:ViewComponent
    {
        readonly IQuestionAnswerService _questionAnswerService;
        public AdminHomeQuestionAnswerCounter(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var questionAnswerCount = _questionAnswerService.QuestionAnswerCounter();
                ViewData["questionAnswerCount"] = questionAnswerCount >= 0 ? questionAnswerCount : 0;
            }
            catch (Exception)
            {
                ViewData["questionAnswerCount"] = 0;
            }
            return View();
        }
    }
}
