using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductDetailImages:ViewComponent
    {
        readonly IPictureService _pictureService;
        public ExplorerProductDetailImages(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingExplorerProductDetailImageByProductId(id));
        }
    }
}
