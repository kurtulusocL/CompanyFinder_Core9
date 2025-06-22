using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ProductCommentAnswer : ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public ProductCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswersForExplorerByCommentId(id));
        }
    }
}
