using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserBlogComment:ViewComponent
    {
        readonly ICommentService _commentService;
        public CompanyUserBlogComment(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.GetAllIncludingCommentForCompanyBlogByBlogId(id));
        }
    }
}
