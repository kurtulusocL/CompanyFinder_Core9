using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeBlogs:ViewComponent
    {
        readonly IBlogService _blogService;
        public CompanyUserHomeBlogs(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_blogService.GetAllIncludingCompanyBlogs(id));
        }
    }
}
