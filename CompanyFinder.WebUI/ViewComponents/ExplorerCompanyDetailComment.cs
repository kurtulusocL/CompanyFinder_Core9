using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyDetailComment:ViewComponent
    {
        readonly ICommentService _commentService;
        public ExplorerCompanyDetailComment(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.GetAllIncludingCommentsForExplorerCompanyDetailByCompanyId(id));
        }
    }
}
