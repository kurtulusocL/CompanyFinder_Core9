using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerBlogDetailImage:ViewComponent
    {
        readonly IPictureService _pictureService;
        public ExplorerBlogDetailImage(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingExplorerBlogDetailImageByBlogId(id));
        }
    }
}
