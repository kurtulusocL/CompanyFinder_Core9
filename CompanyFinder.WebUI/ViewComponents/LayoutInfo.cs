using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class LayoutInfo:ViewComponent
    {
        readonly ILayoutInfoService _layoutInfoService;
        public LayoutInfo(ILayoutInfoService layoutInfoService)
        {
            _layoutInfoService = layoutInfoService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_layoutInfoService.GetAllLayoutInfo());
        }
    }
}
