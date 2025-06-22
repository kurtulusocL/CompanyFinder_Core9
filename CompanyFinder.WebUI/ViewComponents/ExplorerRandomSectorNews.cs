using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerRandomSectorNews : ViewComponent
    {
        readonly ISectorNewsService _sectorNewsService;
        public ExplorerRandomSectorNews(ISectorNewsService sectorNewsService)
        {
            _sectorNewsService = sectorNewsService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_sectorNewsService.GetAllIncludingRandomSectorNews());
        }
    }
}
