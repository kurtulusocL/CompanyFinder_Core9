using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class NewsHit : ViewComponent
    {
        readonly INewsService _newsService;
        public NewsHit(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_newsService.HitRead(id));
        }
    }
}
