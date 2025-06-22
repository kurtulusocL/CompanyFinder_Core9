using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeQuestionCounter : ViewComponent
    {
        readonly IQuestionService _questionService;
        public HomeQuestionCounter(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var questionCount = _questionService.QuestionCounter();
                ViewData["questionCount"] = questionCount >= 0 ? questionCount : 0;
            }
            catch (Exception)
            {
                ViewData["questionCount"] = 0;
            }
            return View();
        }
    }
}
