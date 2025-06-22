using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogCommentForm:ViewComponent
    {
        readonly IBlogService _blogService;
        public ExplorerBlogCommentForm(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.BlogId = _blogService.GetBlogById(id);
            Comment model = new Comment
            {
                BlogId = id
            };
            return View(model);
        }
    }
}
