using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeCommentAnswerCounter : ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public AdminHomeCommentAnswerCounter(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var commentAnswerCount = _commentAnswerService.CommentAnswerCounter();
                ViewData["commentAnswerCount"] = commentAnswerCount >= 0 ? commentAnswerCount : 0;
            }
            catch (Exception)
            {
                ViewData["commentAnswerCount"] = 0;
            }
            return View();
        }
    }
}
