using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerNewsForNewsDetail : ViewComponent
    {
        readonly INewsService _newsService;
        public ExplorerNewsForNewsDetail(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_newsService.GetAllNewsForExplorerNewsDetail());
        }
    }
}
