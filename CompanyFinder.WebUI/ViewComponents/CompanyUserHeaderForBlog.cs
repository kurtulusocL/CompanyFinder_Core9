using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHeaderForBlog:ViewComponent
    {
        readonly IBlogService _blogService;
        public CompanyUserHeaderForBlog(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_blogService.GetIncludingBlogForHeaderByCompanyId(id));
        }
    }
}
