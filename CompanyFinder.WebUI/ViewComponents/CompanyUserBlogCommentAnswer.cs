using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserBlogCommentAnswer:ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public CompanyUserBlogCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswerForCompanyByCommentId(id));
        }
    }
}
