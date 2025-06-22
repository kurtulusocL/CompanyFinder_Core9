using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProductQuestion:ViewComponent
    {
        readonly IQuestionService _questionService;
        public CompanyUserProductQuestion(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_questionService.GetAllIncludingProductQuestionByProductId(id));
        }
    }
}
