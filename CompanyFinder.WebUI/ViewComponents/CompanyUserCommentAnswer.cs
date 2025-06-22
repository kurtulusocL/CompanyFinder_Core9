using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserCommentAnswer:ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public CompanyUserCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswerForCompanyByCommentId(id));
        }
    }
}
