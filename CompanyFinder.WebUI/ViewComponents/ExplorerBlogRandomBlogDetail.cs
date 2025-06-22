using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogRandomBlogDetail : ViewComponent
    {
        readonly IBlogService _blogService;
        public ExplorerBlogRandomBlogDetail(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_blogService.GetAllIncludingRandomBlog());
        }
    }
}
