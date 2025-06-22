using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogDetailCompany : ViewComponent
    {
        readonly IBlogService _blogService;
        public ExplorerBlogDetailCompany(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_blogService.GetIncludeBlogCompanyInfoForBlogDetailByCompanyId(id));
        }
    }
}
