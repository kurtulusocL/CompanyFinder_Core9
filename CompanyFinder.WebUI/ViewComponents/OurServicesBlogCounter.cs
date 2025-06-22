using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class OurServicesBlogCounter:ViewComponent
    {
        readonly IBlogService _blogService;
        public OurServicesBlogCounter(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var blogCount = _blogService.BlogCounter();
                ViewData["blogCount"] = blogCount >= 0 ? blogCount : 0;
            }
            catch (Exception)
            {
                ViewData["blogCount"] = 0;
            }
            return View();
        }
    }
}
