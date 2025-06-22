using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class QuestionHit : ViewComponent
    {
        readonly IQuestionService _questionService;
        public QuestionHit(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_questionService.HitRead(id));
        }
    }
}
