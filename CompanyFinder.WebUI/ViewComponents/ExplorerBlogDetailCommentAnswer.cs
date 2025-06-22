using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogDetailCommentAnswer : ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public ExplorerBlogDetailCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswersForExplorerByCommentId(id));
        }
    }
}
