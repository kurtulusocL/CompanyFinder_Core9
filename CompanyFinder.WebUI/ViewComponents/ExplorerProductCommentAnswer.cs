using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductCommentAnswer:ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public ExplorerProductCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswersForExplorerByCommentId(id));
        }
    }
}
