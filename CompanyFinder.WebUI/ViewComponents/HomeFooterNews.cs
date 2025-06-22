using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeFooterNews : ViewComponent
    {
        readonly INewsService _newsService;
        public HomeFooterNews(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_newsService.GetAllNewsForFooter());
        }
    }
}
