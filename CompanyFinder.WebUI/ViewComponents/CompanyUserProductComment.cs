using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProductComment:ViewComponent
    {
        readonly ICommentService _commentService;
        public CompanyUserProductComment(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.GetAllIncludingCommentForCompanyProductByProductId(id));
        }
    }
}
