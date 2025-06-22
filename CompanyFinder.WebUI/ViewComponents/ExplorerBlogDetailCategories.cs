using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogDetailCategories : ViewComponent
    {
        readonly IBlogCategoryService _blogCategoryService;
        public ExplorerBlogDetailCategories(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_blogCategoryService.GetAllIncludingBlogCategories());
        }
    }
}
