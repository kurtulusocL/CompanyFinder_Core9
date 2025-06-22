using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeCommentCounter : ViewComponent
    {
        readonly ICommentService _commentService;
        public AdminHomeCommentCounter(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var commentCount = _commentService.CommentCounter();
                ViewData["commentCount"] = commentCount >= 0 ? commentCount : 0;
            }
            catch (Exception)
            {
                ViewData["commentCount"] = 0;
            }
            return View();
        }
    }
}
