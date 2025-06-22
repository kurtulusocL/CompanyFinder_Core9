using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class NewsHitExplorer : ViewComponent
    {
        readonly INewsService _newsService;
        public NewsHitExplorer(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_newsService.HitRead(id));
        }
    }
}
