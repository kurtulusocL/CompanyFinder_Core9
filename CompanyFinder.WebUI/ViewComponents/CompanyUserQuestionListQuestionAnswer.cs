using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserQuestionListQuestionAnswer : ViewComponent
    {
        readonly IQuestionAnswerService _questionAnswerService;
        public CompanyUserQuestionListQuestionAnswer(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_questionAnswerService.GetAllIncludingQuestionAnswerByQuestionId(id));
        }
    }
}
