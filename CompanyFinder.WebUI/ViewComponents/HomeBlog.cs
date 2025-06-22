using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeBlog:ViewComponent
    {
        readonly IBlogService _blogService;
        public HomeBlog(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_blogService.GetAllIncludingRandomBlogForHome());
        }
    }
}
