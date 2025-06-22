using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogDetailComment:ViewComponent
    {
        readonly ICommentService _commentService;
        public ExplorerBlogDetailComment(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.GetAllIncludingCommentsForExplorerBlogDetailByBlogId(id));
        }
    }
}
