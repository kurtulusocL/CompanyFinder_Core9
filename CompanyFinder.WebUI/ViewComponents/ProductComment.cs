using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ProductComment : ViewComponent
    {
        readonly ICommentService _commentService;
        public ProductComment(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.GetAllIncludingCommentsForExplorerProductDetailByProductId(id));
        }
    }
}
